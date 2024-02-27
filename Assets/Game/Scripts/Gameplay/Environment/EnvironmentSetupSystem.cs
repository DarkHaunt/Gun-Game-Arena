using Game.Scripts.Extensions;
using Game.Scripts.Gameplay.Entities.Creation;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Level;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Environment
{
    public class EnvironmentSetupSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<EntitiesFactory> _entitiesFactory = default;
        private readonly EcsCustomInject<Camera> _camera = default;
        
        private readonly EcsWorldInject _world = default;


        public void Init(IEcsSystems systems)
        {
            var world = _world.Value;
            var level = SpawnLevel();
            
            var player = _entitiesFactory.Value.SetUpPlayer(world, level.PlayerSpawnPoint.position);
            _entitiesFactory.Value.SetUpEnemy(world, level.EnemiesSpawnPoints.PickRandom().position);
            
            SetUpCamera(world, player.transform);
        }

        private void SetUpCamera(EcsWorld world, Transform followTarget)
        {
            var camera = world.NewEntity();
            var config = Resources.Load<EnvironmentConfig>(Indents.Path.EnvironmentConfigPath);

            ref var follower = ref world.GetPool<FollowTarget>().Add(camera);
            follower.Self = _camera.Value.transform;
            follower.Offset = config.CameraOffset;
            follower.Target = followTarget;
        }

        private LevelView SpawnLevel()
        {
            var view = Object.Instantiate(Resources.Load<LevelView>(Indents.Path.LevelViewPath));

            return view;
        }
    }
}