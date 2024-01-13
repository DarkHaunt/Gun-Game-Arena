using Voody.UniLeo.Lite;
using Leopotam.EcsLite;
using Zenject;
using System;

namespace Game.Scripts.Gameplay
{
    public class GameplayEscHandler : ITickable, IDisposable, IInitializable
    {
        private readonly EcsSystems _systems;
        

        public GameplayEscHandler(EcsWorld world)
        {
            _systems = new EcsSystems(world);
        }
        

        public void Initialize()
        {
            _systems.Init();
            _systems.ConvertScene();
        }

        public void Tick()
            => _systems.Run();

        public void Dispose()
            => _systems.Destroy();
    }
}