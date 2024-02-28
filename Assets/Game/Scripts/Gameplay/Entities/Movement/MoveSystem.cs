using Game.Scripts.Gameplay.Entities.Physics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Entities.Movement
{
    public class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Physical2D, Move>> _filter = default;
        
        private readonly EcsPoolInject<Physical2D> _physicPool = default;
        private readonly EcsPoolInject<Move> _movePool = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var physic = ref _physicPool.Value.Get(entity);
                ref var walk = ref _movePool.Value.Get(entity);
                
                physic.Body.AddForce(walk.Direction * walk.Speed);
            }
        }
    }
}