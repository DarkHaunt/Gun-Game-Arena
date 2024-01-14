using Game.Scripts.Gameplay.Input.Event_Handling;
using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Jump;
using Game.Scripts.Gameplay.Move.Walk;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using Zenject;
using System;
using static Game.Scripts.Gameplay.StaticData.StaticData;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld defaultWorld, WalkSystem walkSystem, InputMoveHandleSystem moveHandleSystem, 
            JumpSystem jumpSystem, InputEventsCleanUpSystem cleanUpSystem, InputEventsSendSystem sendSystem)
        {
            _systems = new EcsSystems(defaultWorld);
            _systems
                .AddWorld(new EcsWorld(), EventWorld)
                .Add(sendSystem)
                .Add(moveHandleSystem)
                .Add(jumpSystem)
                .Add(walkSystem)
                .Add(cleanUpSystem);
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