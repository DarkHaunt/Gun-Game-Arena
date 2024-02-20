using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Physic;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Move.Walk
{
    public class WalkSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Physical2D, Walk, InputMove>> _filter = default;
        
        private readonly EcsPoolInject<Physical2D> _physicPool = default;
        private readonly EcsPoolInject<InputMove> _inputPool = default;
        private readonly EcsPoolInject<Walk> _walkPool = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var physic = ref _physicPool.Value.Get(entity);
                ref var input = ref _inputPool.Value.Get(entity);
                ref var walk = ref _walkPool.Value.Get(entity);
                
                physic.Body.AddForce(input.Direction * walk.Speed);
            }
        }
    }
}