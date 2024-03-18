using Game.Scripts.Extensions;
using Game.Scripts.Gameplay.Enemy.Follow;
using Game.Scripts.Gameplay.Entities.Enemy.Base;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Player.Targeting;
using Game.Scripts.Gameplay.Entities.Physics;
using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.HealthHandling;
using Game.Scripts.Gameplay.Entities.Enemy;
using Game.Scripts.Infrastructure.Assets;
using Game.Scripts.Gameplay.Player.Base;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Weapons;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite;
using UnityEngine;


namespace Game.Scripts.Gameplay.Entities.Creation
{
    public class EntitiesFactory : IEcsSystem
    {
        private readonly AssetProvider _assetProvider;

        public EntitiesFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public PlayerView CreatePlayer(EcsWorld world, Vector3 pos)
        {
            var config = _assetProvider.GetSync<PlayerConfig>(Indents.Path.PlayerConfigPath);
            var prefab = _assetProvider.GetSync<PlayerView>(Indents.Path.PlayerViewPath);
            var player = world.NewEntity();
            
            var view = Object.Instantiate(prefab);
            view.Construct(world.PackEntity(player));
            view.transform.SetInPosition(pos);

            world.GetPool<InputListenerTag>().Add(player);
            world.GetPool<WeaponHandler>().Add(player);
            world.GetPool<PlayerTag>().Add(player);
            
            ref var health  = ref world.GetPool<HealthData>().Add(player);
            health.Init(config.Health);
            
            ref var attack  = ref world.GetPool<AttackData>().Add(player);
            attack.Distance = config.AttackDistance;
            attack.Cooldown = config.Cooldown;
            attack.Damage = config.Damage;
            
            ref var target  = ref world.GetPool<Target>().Add(player);
            target.Self = view.transform;

            ref var walk = ref world.GetPool<Move>().Add(player);
            walk.Speed = config.MoveForce;

            ref var physic = ref world.GetPool<Physical2D>().Add(player);
            physic.Collider = view.Collider;
            physic.Body = view.Rigidbody;

            return view;
        } 
        
        public void CreateEnemy(EcsWorld world, Transform followTarget, Vector3 pos)
        {
            var config = _assetProvider.GetSync<PlayerConfig>(Indents.Path.EnemyConfigPath);
            var prefab = _assetProvider.GetSync<EnemyView>(Indents.Path.EnemyViewPath);
            
            var view = Object.Instantiate(prefab);
            var enemy = world.NewEntity();
            
            view.Construct(world.PackEntity(enemy));
            view.transform.SetInPosition(pos);

            world.GetPool<EnemyTag>().Add(enemy);
            
            ref var follow  = ref world.GetPool<EnemyTargetFollower>().Add(enemy);
            follow.StopFollowDistance = config.AttackDistance;
            follow.Self = view.transform;
            follow.Target = followTarget;
            
            ref var health  = ref world.GetPool<HealthData>().Add(enemy);
            health.Init(config.Health);
            
            ref var target  = ref world.GetPool<Target>().Add(enemy);
            target.Self = view.transform;

            ref var walk = ref world.GetPool<Move>().Add(enemy);
            walk.Speed = config.MoveForce;

            ref var physic = ref world.GetPool<Physical2D>().Add(enemy);
            physic.Collider = view.Collider;
            physic.Body = view.Rigidbody;
        }
    }
}