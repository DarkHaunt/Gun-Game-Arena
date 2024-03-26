using Game.Scripts.Extensions;
using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.Projectiles.Creation;
using Game.Scripts.Gameplay.Entities.Cooldown;
using Game.Scripts.Gameplay.Player.Targeting;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Weapons.Shooting
{
    public class ShootingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, ShootingData>, Exc<CooldownRequest>> _filter;
        private readonly EcsWorldInject _world;

        private readonly EcsCustomInject<ProjectileFactory> _projectileFactory;

        private readonly EcsPoolInject<CooldownRequest> _cooldownRequestPool;
        private readonly EcsPoolInject<ShootingData> _shootingPool;
        private readonly EcsPoolInject<AttackRequest> _attackPool;
        private readonly EcsPoolInject<Target> _targetPool;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var shooting = ref _shootingPool.Value.Get(entity);
                ref var attack = ref _attackPool.Value.Get(entity);

                attack.Target.Unpack(_world.Value, out int attackTarget);
                var target = _targetPool.Value.Get(attackTarget);
                
                _projectileFactory.Value.CreateProjectile
                (
                    direction: Vector3Extensions.Direction(shooting.ShootPoint.position, target.Self.position),
                    pos: shooting.ShootPoint.position,
                    config: shooting.ProjectileConfig
                );

                ref var cooldown = ref _cooldownRequestPool.Value.Add(entity);
                cooldown.Time = shooting.Cooldown;
            }
        }
    }
}