using Game.Scripts.Gameplay.Input;
using Game.Scripts.Gameplay.Moving;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Zenject;
using System;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;


        public GameplayEscHandler(EcsWorld world, MoveSystem moveSystem, InputSystem inputSystem)
        {
            _systems = new EcsSystems(world);

            _systems.Add(inputSystem);
            _systems.Add(moveSystem);
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