using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using static Game.Scripts.Gameplay.StaticData.StaticData;

namespace Game.Scripts.Gameplay.Input.Event_Handling
{
    public class InputEventsCleanUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEvent>> _attackFilter = EventWorld;
        private readonly EcsFilterInject<Inc<JumpEvent>> _jumpFilter = EventWorld;
        private readonly EcsFilterInject<Inc<DownEvent>> _downFilter = EventWorld;
        
        private readonly EcsPoolInject<AttackEvent> _attackPool = EventWorld;
        private readonly EcsPoolInject<JumpEvent> _jumpPool = EventWorld;
        private readonly EcsPoolInject<DownEvent> _downPool = EventWorld;
        
        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _jumpFilter.Value)
                _jumpPool.Value.Del(i);       
            
            foreach (var i in _attackFilter.Value)
                _attackPool.Value.Del(i); 
            
            foreach (var i in _downFilter.Value)
                _downPool.Value.Del(i);
        }
    }
}