using Game.Scripts.Gameplay.HealthHandling;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Entities.Physics;
using Game.Scripts.Gameplay.Entities.Player;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Environment
{
    public class EnvironmentSetupSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Camera> _camera;

        private readonly EcsWorldInject _world = default;


        public void Init(IEcsSystems systems)
        {
            var player = SetUpPlayer();
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

            ref var walk = ref world.GetPool<Move>().Add(player);
            walk.Speed = config.MoveForce;

            ref var physic = ref world.GetPool<Physical2D>().Add(player);
            physic.Collider = view.Collider;
            physic.Body = view.Rigidbody;

            return view;
        }
    }
}