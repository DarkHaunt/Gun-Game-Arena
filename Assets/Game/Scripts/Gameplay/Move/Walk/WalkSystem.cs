using Game.Scripts.Gameplay.Input.Move;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Walk
{
    public class WalkSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<InputMove> _inputPool = default;
        private readonly EcsPoolInject<Walk> _walkPool = default;
        
        private readonly EcsFilterInject<Inc<InputMove, Walk>> _filter = default;

        private Vector2 _cachedForce;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var walkParams = ref _walkPool.Value.Get(entity);
                ref var inputParams = ref _inputPool.Value.Get(entity);
                
                _cachedForce.Set(inputParams.XDirection * walkParams.Speed, 0f);

                walkParams.Body.AddForce(_cachedForce); 
            }
        }
        
    }
}