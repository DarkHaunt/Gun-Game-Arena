using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Entities.Enemy;
using Game.Scripts.Extensions;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Enemy.Follow
{
    public class EnemyTargetFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Move, EnemyTag, EnemyTargetFollower>> _enemyFilter = default;
        
        private readonly EcsPoolInject<EnemyTargetFollower> _targets = default;
        private readonly EcsPoolInject<Move> _moves = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _enemyFilter.Value)
            {
                ref var target = ref _targets.Value.Get(entity);
                ref var move = ref _moves.Value.Get(entity);

                move.Direction = Vector3Extensions.Direction(target.SelfPos, target.TargetPos);
            }
        }
    }
}