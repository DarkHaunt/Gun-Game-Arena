using Game.Scripts.Gameplay.HealthHandling;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.Entities.Enemy;
using Game.Scripts.Gameplay.Entities.Enemy.Base;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Entities.Physics;
using Game.Scripts.Gameplay.Input;
using Game.Scripts.Gameplay.Player.Base;
using Game.Scripts.Gameplay.Player.Targeting;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Environment
{
    public class EnvironmentSetupSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Camera> _camera = default;
        
        private readonly EcsWorldInject _world = default;


        public void Init(IEcsSystems systems)
        {
            var player = SetUpPlayer();
            SetUpEnemy();
            
            SetUpCamera(player.transform);
        }

        private void SetUpCamera(Transform followTarget)
        {
            var world = _world.Value;
            var camera = world.NewEntity();
            var config = Resources.Load<EnvironmentConfig>(Indents.Path.EnvironmentConfigPath);

            ref var follower = ref world.GetPool<FollowTarget>().Add(camera);
            follower.Self = _camera.Value.transform;
            follower.Offset = config.CameraOffset;
            follower.Target = followTarget;
        }
        
        private PlayerView SetUpPlayer()
        {
            var view = Object.Instantiate(Resources.Load<PlayerView>(Indents.Path.PlayerViewPath));
            var config = Resources.Load<PlayerConfig>(Indents.Path.PlayerConfigPath);

            var world = _world.Value;
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
        
        private void SetUpEnemy()
        {
            var view = Object.Instantiate(Resources.Load<EnemyView>(Indents.Path.EnemyViewPath));
            var config = Resources.Load<PlayerConfig>(Indents.Path.PlayerConfigPath);
            
            view.transform.Translate(new Vector3(3f, 0f));

            var world = _world.Value;
            var enemy = world.NewEntity();

            world.GetPool<EnemyTag>().Add(enemy);
            
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