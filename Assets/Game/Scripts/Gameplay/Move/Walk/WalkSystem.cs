using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Moving
{
    public class WalkSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<InputParams> _inputPool = default;
        private readonly EcsPoolInject<WalkParams> _walkPool = default;
        
        private readonly EcsFilterInject<Inc<InputParams, WalkParams>> _filter = default;

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