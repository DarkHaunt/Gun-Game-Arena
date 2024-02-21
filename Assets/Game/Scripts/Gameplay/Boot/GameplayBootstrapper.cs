using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Input.Events;
using Game.Scripts.Gameplay.Environment;
using Leopotam.EcsLite.ExtendedSystems;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using LeoEcsPhysics;
using UnityEngine;
using Zenject;
using System;
using Game.Scripts.Gameplay.Entities.Damage;
using Game.Scripts.Gameplay.Entities.Movement;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayBootstrapper : IInitializable, IFixedTickable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly InputActions _inputActions;
        private readonly Camera _camera;
        
        private EcsSystems _fixedUpdateSystems;

        public GameplayBootstrapper(GameStateMachine gameStateMachine, InputActions inputActions, Camera camera)
        {
            _gameStateMachine = gameStateMachine;
            _inputActions = inputActions;
            _camera = camera;
        }


        public async void Initialize()
        {
            await _gameStateMachine.Enter<GameplayState>();
 
            var physicWorld = new EcsWorld();

            _fixedUpdateSystems = new EcsSystems(physicWorld);
            _fixedUpdateSystems
                .Add(new EnvironmentSetupSystem())
                .Add(new InputHandleSystem())
                .Add(new MoveSystem())
                .Add(new FollowSystem())
                .Add(new DamageApplySystem());
            
            EcsPhysicsEvents.ecsWorld = physicWorld;

            SetUpCleanupEvents();
            
            _fixedUpdateSystems
                .ConvertScene()
                .Inject(_inputActions, _camera)
                .Init();
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHere<DamageRequest>()
                .DelHere<AttackEvent>()
                .DelHerePhysics();
        }

        public void FixedTick()
            => _fixedUpdateSystems?.Run();

        public void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;
            
            _fixedUpdateSystems.Destroy();
            _fixedUpdateSystems
                .GetWorld()
                .Destroy();
        }
    }
}