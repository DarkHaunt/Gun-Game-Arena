using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Moving;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using Zenject;
using System;
using Game.Scripts.Gameplay.Input.Events;
using static Game.Scripts.Gameplay.StaticData.GameplayStaticData;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld defaultWorld, WalkSystem walkSystem, InputSystem inputSystem, 
            JumpSystem jumpSystem, EventsCleanUpSystem cleanUpSystem)
        {
            _systems = new EcsSystems(defaultWorld);

            _systems
                .AddWorld(new EcsWorld(), EventWorld)
                .Add(inputSystem)
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
            => _systems.Destroy();
    }
}