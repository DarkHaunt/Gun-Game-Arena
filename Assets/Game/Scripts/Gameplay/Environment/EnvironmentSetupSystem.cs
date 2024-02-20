using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Input.Event_Handling;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Walk;
using Game.Scripts.Gameplay.Physic;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.Gameplay.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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

            world.GetPool<InputEventsListener>().Add(player);
            world.GetPool<PlayerTag>().Add(player);
            world.GetPool<InputMove>().Add(player);

            ref var walk = ref world.GetPool<Walk>().Add(player);
            walk.Speed = config.MoveForce;

            ref var physic = ref world.GetPool<Physical2D>().Add(player);
            physic.Collider = view.Collider;
            physic.Body = view.Rigidbody;

            return view;
        }
    }
}