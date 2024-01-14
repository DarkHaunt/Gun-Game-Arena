using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Moving;
using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using Zenject;
using System;
using static Game.Scripts.Gameplay.StaticData.GameplayStaticData;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld defaultWorld, WalkSystem walkSystem, InputSystem inputSystem)
        {
            _systems = new EcsSystems(defaultWorld);

            _systems.AddWorld(new EcsWorld(), EventWorld);
            _systems.Add(inputSystem);
            _systems.Add(walkSystem);
        }

        
        public void Initialize()
        {
            var eventWorld = new EcsWorld();
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