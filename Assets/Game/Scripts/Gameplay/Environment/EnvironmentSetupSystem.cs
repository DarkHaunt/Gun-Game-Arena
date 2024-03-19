using Game.Scripts.Extensions;
using Game.Scripts.Gameplay.Entities.Creation;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Level;
using Game.Scripts.Gameplay.Weapons.Creation;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Environment
{
    public class EnvironmentSetupSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<EntitiesFactory> _entitiesFactory = default;
        private readonly EcsCustomInject<WeaponFactory> _weaponFactory = default;
        
        private readonly EcsCustomInject<EnvironmentConfig> _config = default;
        private readonly EcsCustomInject<EnvironmentData> _data = default;
        
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var level = SpawnLevel();
            
            var player = _entitiesFactory.Value.CreatePlayer(world, level.PlayerSpawnPoint.position);
            _entitiesFactory.Value.CreateEnemy(world, player.transform, level.EnemiesSpawnPoints.PickRandom().position);

            _weaponFactory.Value.CreateRandomWeapon();
            
            SetUpCamera(world, player.transform);
        }

        private void SetUpCamera(EcsWorld world, Transform followTarget)
        {
            var camera = world.NewEntity();

            ref var follower = ref world.GetPool<CameraFollow>().Add(camera);
            
            follower.Self = _data.Value.Camera.transform;
            follower.Offset = _config.Value.CameraOffset;
            follower.Target = followTarget;
        }

        private LevelView SpawnLevel()
        {
            var view = Object.Instantiate(Resources.Load<LevelView>(PathIndents.LevelViewPath));

            return view;
        }
    }
}