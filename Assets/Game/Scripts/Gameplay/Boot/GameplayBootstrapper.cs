using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Gameplay.Entities.Creation;
using Game.Scripts.Gameplay.Entities.Cooldown;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Player.Targeting;
using Game.Scripts.Gameplay.Entities.Damage;
using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.Environment;
using Leopotam.EcsLite.ExtendedSystems;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Input;
using Game.Scripts.Gameplay.Time;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Enemy.Follow;
using Game.Scripts.Gameplay.Entities.Enemy.Base;
using Game.Scripts.Gameplay.Player.Base;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Infrastructure.Assets;
using LeoEcsPhysics;
using Zenject;
using System;
using Game.Scripts.Gameplay.Weapons;
using Game.Scripts.Gameplay.Weapons.Configs;
using Game.Scripts.Gameplay.Weapons.Creation;
using Game.Scripts.Gameplay.Weapons.Overlap;
using Leopotam.EcsLite.UnityEditor;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayBootstrapper : IInitializable, IFixedTickable, ITickable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AssetProvider _assetProvider;
        private readonly InputActions _inputActions;
        
        private readonly EnvironmentData _environmentData;
        
        private AvailableWeaponsConfig _availableWeaponsConfig;
        private EnvironmentConfig _environmentConfig;
        
        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _updateSystems;
        
        private EcsWorld _defaultWorld;


        public GameplayBootstrapper(GameStateMachine gameStateMachine, AssetProvider assetProvider, InputActions inputActions, 
            EnvironmentData environmentData)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _inputActions = inputActions;
            
            _environmentData = environmentData;
        }


        public async void Initialize()
        {
            var prewarms = new[]
            {
                _assetProvider.LoadAndCacheAsset<EnemyView>(PathIndents.EnemyViewPath),
                _assetProvider.LoadAndCacheAsset<PlayerConfig>(PathIndents.EnemyConfigPath),
                
                _assetProvider.LoadAndCacheAsset<PlayerView>(PathIndents.PlayerViewPath),
                _assetProvider.LoadAndCacheAsset<PlayerConfig>(PathIndents.PlayerConfigPath),
            };
            
            _availableWeaponsConfig = await _assetProvider.Get<AvailableWeaponsConfig>(PathIndents.AvailableWeaponsConfigPath);
            _environmentConfig = await _assetProvider.Get<EnvironmentConfig>(PathIndents.EnvironmentConfigPath);

            await UniTask.WhenAll(prewarms);
            
            await _gameStateMachine.Enter<GameplayState>();
            
            CreateSystems();
            SetUpCleanupEvents();
            InjectInit();
        }

        private void CreateSystems()
        {
            _defaultWorld = new EcsWorld();

            _fixedUpdateSystems = new EcsSystems(_defaultWorld);
            _fixedUpdateSystems
                .Add(new EnvironmentSetupSystem())
                .Add(new OverlapCircleSystem())
                .Add(new EnemyTargetFollowSystem())
                .Add(new MoveSystem())
                .Add(new CameraFollowSystem())
#if UNITY_EDITOR
                .Add (new EcsWorldDebugSystem ())
                .Add (new EcsSystemsDebugSystem ())
#endif
                ;

            _updateSystems = new EcsSystems(_defaultWorld);
            _updateSystems
                .Add(new TimeSystem())
                .Add(new InputHandleSystem())
                .Add(new CooldownSystem())
                .Add(new TargetCheckSystem())
                
                // Weapons Feature
                .Add(new WeaponHandleSystem())
                .Add(new WeaponSwitchSystem())
                
                // Attack Feature
                .Add(new AttackSystem())
                .Add(new DamageApplySystem())
                
#if UNITY_EDITOR
                .Add (new EcsWorldDebugSystem ())
                .Add (new EcsSystemsDebugSystem ())
#endif
                ;
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHere<OverlapCircleRequest>()
                .DelHere<OverlappedTag>()
                .DelHerePhysics();

            _updateSystems
                .DelHere<WeaponSwitchRequest>()
                    
                .DelHere<TargetCheckRequest>()
                .DelHere<DamageRequest>()
                .DelHere<AttackRequest>()
                ;
        }

        private void InjectInit()
        {
            var weaponFactory = new WeaponFactory(_defaultWorld,_availableWeaponsConfig);
            var entitiesFactory = new EntitiesFactory(_assetProvider, weaponFactory);
            var timeService = new TimeService();
            
            _updateSystems
                .Inject(_inputActions, weaponFactory, timeService)
                .Init();
            
            _fixedUpdateSystems
                .Inject(_environmentData, _environmentConfig, entitiesFactory, weaponFactory)
                .Init();
        }

        public void FixedTick()
            => _fixedUpdateSystems?.Run();

        public void Tick()
            => _updateSystems?.Run();

        public void Dispose()
        {
            _updateSystems.Destroy();
            _updateSystems
                .GetWorld()
                .Destroy();
            
            _fixedUpdateSystems.Destroy();
            _fixedUpdateSystems
                .GetWorld()
                .Destroy();
        }
    }
}