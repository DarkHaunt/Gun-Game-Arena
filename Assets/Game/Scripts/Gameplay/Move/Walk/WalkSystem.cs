using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Physic;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Walk
{
    public class WalkSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<InputMove> _inputPool = default;
        private readonly EcsPoolInject<Physic2D> _physicPool = default;
        private readonly EcsPoolInject<Walk> _walkPool = default;
        
        private readonly EcsFilterInject<Inc<Physic2D, Walk, InputMove>> _filter = default;
        

        private Vector2 _cachedForce;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var walk = ref _walkPool.Value.Get(entity);
                ref var input = ref _inputPool.Value.Get(entity);
                ref var physic = ref _physicPool.Value.Get(entity);
                
                _cachedForce.Set(input.XDirection * walk.Speed, 0f);

                physic.Body.AddForce(_cachedForce);
            }
        }
        
    }
}