using Game.Scripts.Gameplay.Moving;
using Game.Scripts.Gameplay.Input;
using Voody.UniLeo.Lite;
using Leopotam.EcsLite;
using Zenject;
using System;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld world, WalkSystem walkSystem, InputSystem inputSystem)
        {
            _systems = new EcsSystems(world);

            _systems.Add(inputSystem);
            _systems.Add(walkSystem);
        }

        
        public void Initialize()
        {
            _systems
                .ConvertScene()
                .Init();
        }

        public void Tick()
            => _systems.Run();

        public void Dispose()
            => _systems.Destroy();
    }
}