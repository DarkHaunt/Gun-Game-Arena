using Game.Scripts.Gameplay.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Input.Events
{
    public class EventsCleanUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<JumpEvent>> _jumpEvents = GameplayStaticData.EventWorld;
        private readonly EcsPoolInject<JumpEvent> _pool = GameplayStaticData.EventWorld;
        
        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _jumpEvents.Value)
                _pool.Value.Del(i);
        }
    }
}