using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Gameplay.Entities.Cooldown;
using Game.Scripts.Gameplay.Entities.Movement;
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
using LeoEcsPhysics;
using UnityEngine;
using Zenject;
using System;
using Game.Scripts.Gameplay.Player.Targeting;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayBootstrapper : IInitializable, IFixedTickable, ITickable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly InputActions _inputActions;
        private readonly Camera _camera;
        
        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _updateSystems;

        
        public GameplayBootstrapper(GameStateMachine gameStateMachine, InputActions inputActions, Camera camera)
        {
            _gameStateMachine = gameStateMachine;
            _inputActions = inputActions;
            _camera = camera;
        }


        public async void Initialize()
        {
            await _gameStateMachine.Enter<GameplayState>();
            
            CreateSystems();
            SetUpCleanupEvents();
            InjectInit();
        }

        private void CreateSystems()
        {
            var defaultWorld = new EcsWorld();

            _fixedUpdateSystems = new EcsSystems(defaultWorld);
            _fixedUpdateSystems
                .Add(new EnvironmentSetupSystem())
                .Add(new MoveSystem())
                .Add(new FollowSystem())
                ;

            _updateSystems = new EcsSystems(defaultWorld);
            _updateSystems
                .Add(new TimeSystem())
                .Add(new InputHandleSystem())
                .Add(new CooldownSystem())
                .Add(new TargetCheckSystem())
                .Add(new AttackSystem())
                .Add(new DamageApplySystem())
                ;
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHerePhysics();

            _updateSystems
                .DelHere<TargetCheckRequest>()
                .DelHere<DamageRequest>()
                .DelHere<AttackRequest>()
                ;
        }

        private void InjectInit()
        {
            _updateSystems
                .Inject(_inputActions, new TimeService())
                .Init();

            _fixedUpdateSystems
                .Inject(_camera)
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