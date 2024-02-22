using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Gameplay.Entities.Cooldown;
using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Entities.Damage;
using Game.Scripts.Gameplay.Input.Events;
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
                .Add(new CooldownSystem())
                .Add(new MoveSystem())
                .Add(new FollowSystem())
                .Add(new DamageApplySystem());

            _updateSystems = new EcsSystems(defaultWorld);
            _updateSystems
                .Add(new EnvironmentSetupSystem())
                .Add(new TimeSystem())
                .Add(new InputHandleSystem());
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHere<DamageRequest>()
                .DelHere<AttackEvent>()
                .DelHerePhysics();
        }

        private void InjectInit()
        {
            _updateSystems
                .Inject(_inputActions, new TimeService(), _camera)
                .Init();

            _fixedUpdateSystems
                .Inject()
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