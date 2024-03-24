using Game.Scripts.Gameplay.HealthHandling;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entities.Damage
{
    public class DamageApplySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DamageRequest, HealthData>> _filter;
        
        private readonly EcsPoolInject<DamageRequest> _damagePool;
        private readonly EcsPoolInject<HealthData> _healthPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var health = ref _healthPool.Value.Get(entity);
                ref var damage = ref _damagePool.Value.Get(entity);

                health.Current -= Mathf.Clamp(damage.Damage, 0f, health.Current);
            }
        }
    }
}