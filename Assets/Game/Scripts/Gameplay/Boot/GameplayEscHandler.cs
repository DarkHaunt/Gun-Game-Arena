using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Game.Scripts.Gameplay.Input.Event_Handling;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite.ExtendedSystems;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Jump;
using Game.Scripts.Gameplay.Move.Walk;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using Zenject;
using System;
using static Game.Scripts.Gameplay.StaticData.Indents;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld defaultWorld, WalkSystem walkSystem, InputMoveHandleSystem moveHandleSystem, 
            JumpSystem jumpSystem, InputEventsSendSystem sendSystem)
        {
            _systems = new EcsSystems(defaultWorld);
            _systems
                .AddWorld(new EcsWorld(), EventWorld)
                .Add(sendSystem)
                .Add(moveHandleSystem)
                .Add(jumpSystem)
                .Add(walkSystem);

            SetUpCleanupEvents();
        }


        private void SetUpCleanupEvents()
        {
            _systems
                .DelHere<JumpEvent>()
                .DelHere<AttackEvent>()
                .DelHere<DownEvent>();
        }
        
        public void Initialize()
        {
            _systems
                .ConvertScene()
                .Inject()
                .Init();
        }

        public void Tick()
            => _systems.Run();

        public void Dispose()
        {
            _systems.Destroy();
            
            _systems
                .GetWorld()
                .Destroy();
        }
    }
}