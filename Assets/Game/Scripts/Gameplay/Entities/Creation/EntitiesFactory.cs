using Game.Scripts.Extensions;
using Game.Scripts.Gameplay.Enemy.Follow;
using Game.Scripts.Gameplay.Entities.Enemy.Base;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Player.Targeting;
using Game.Scripts.Gameplay.Entities.Physics;
using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.HealthHandling;
using Game.Scripts.Gameplay.Entities.Enemy;
using Game.Scripts.Gameplay.Player.Base;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite;
using UnityEngine;


namespace Game.Scripts.Gameplay.Entities.Creation
{
    public class EntitiesFactory : IEcsSystem
    {
        public PlayerView CreatePlayer(EcsWorld world, Vector3 pos)
        {
            var view = Object.Instantiate(Resources.Load<PlayerView>(Indents.Path.PlayerViewPath));
            view.transform.SetInPosition(pos);

            var config = Resources.Load<PlayerConfig>(Indents.Path.PlayerConfigPath);
            var player = world.NewEntity();

            world.GetPool<InputListener>().Add(player);
            world.GetPool<PlayerTag>().Add(player);
            
            ref var health  = ref world.GetPool<HealthData>().Add(player);
            health.Init(config.Health);
            
            ref var attack  = ref world.GetPool<AttackData>().Add(player);
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
            var view = Object.Instantiate(Resources.Load<EnemyView>(Indents.Path.EnemyViewPath));
            view.transform.SetInPosition(pos);
            
            var config = Resources.Load<PlayerConfig>(Indents.Path.EnemyConfigPath);
            var enemy = world.NewEntity();

            world.GetPool<EnemyTag>().Add(enemy);
            
            ref var follow  = ref world.GetPool<EnemyTargetFollower>().Add(enemy);
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