using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Input.Event_Handling
{
    public class InputEventsCleanUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEvent>> _attackFilter = default;
        private readonly EcsFilterInject<Inc<JumpEvent>> _jumpFilter = default;
        private readonly EcsFilterInject<Inc<DownEvent>> _downFilter = default;
        
        private readonly EcsPoolInject<AttackEvent> _attackPool = default;
        private readonly EcsPoolInject<JumpEvent> _jumpPool = default;
        private readonly EcsPoolInject<DownEvent> _downPool = default;
        
        
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