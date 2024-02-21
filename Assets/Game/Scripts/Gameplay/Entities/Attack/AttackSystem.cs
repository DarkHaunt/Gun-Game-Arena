using Game.Scripts.Gameplay.Entities.Cooldown;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Entities.Attack
{
    public class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackData>, Exc<CooldownRequest>> _filter = default;
        private readonly EcsPoolInject<AttackData> _pool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var attack = ref _pool.Value.Get(entity);
            }
        }
    }
}