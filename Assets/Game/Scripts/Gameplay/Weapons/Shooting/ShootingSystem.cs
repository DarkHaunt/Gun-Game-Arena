using Game.Scripts.Gameplay.Projectiles.Creation;
using Game.Scripts.Gameplay.Entities.Cooldown;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Weapons.Shooting
{
    public class ShootingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShootingRequest>, Exc<CooldownRequest>> _filter;

        private readonly EcsCustomInject<ProjectileFactory> _projectileFactory;

        private readonly EcsPoolInject<CooldownRequest> _cooldownRequestPool;
        private readonly EcsPoolInject<ShootingRequest> _shootingRequestPool;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var shooting = ref _shootingRequestPool.Value.Get(entity);
                
                _projectileFactory.Value.CreateProjectile
                (
                    pos: shooting.ShootPoint, 
                    direction: shooting.Direction, 
                    config: shooting.ProjectileConfig
                );

                ref var cooldown = ref _cooldownRequestPool.Value.Add(entity);
                cooldown.Time = shooting.Cooldown;
            }
        }
    }
}