using Game.Scripts.Gameplay.Time;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Entities.Cooldown
{
    public class CooldownSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<TimeService> _timeService = default;
        
        private readonly EcsFilterInject<Inc<CooldownRequest>> _filter = default;
        private readonly EcsPoolInject<CooldownRequest> _pool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var cooldown = ref _pool.Value.Get(entity);
                cooldown.Passed += _timeService.Value.DeltaTime;
                
                if(cooldown.Passed >= cooldown.Time)
                    _pool.Value.Del(entity);
            }
        }
    }
}