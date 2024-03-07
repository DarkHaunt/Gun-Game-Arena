using Game.Scripts.Gameplay.Entities.Cooldown;
using Game.Scripts.Gameplay.Entities.Damage;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Entities.Attack
{
    public class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackData, AttackRequest>, Exc<CooldownRequest>> _filter = default;
        
        private readonly EcsPoolInject<CooldownRequest> _cooldownRequestPool = default;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool = default;
        private readonly EcsPoolInject<DamageRequest> _damageRequestPool = default;
        
        private readonly EcsPoolInject<AttackData> _dataPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var request = ref _attackRequestPool.Value.Get(entity);
                ref var attack = ref _dataPool.Value.Get(entity);
                
                var damageEntity = request.Target.Id;
                ref var damage = ref _damageRequestPool.Value.Add(damageEntity);
                damage.Damage = attack.Damage;
                
                ref var cooldown = ref _cooldownRequestPool.Value.Add(entity);
                cooldown.Time = attack.Cooldown;
            }
        }
    }
}