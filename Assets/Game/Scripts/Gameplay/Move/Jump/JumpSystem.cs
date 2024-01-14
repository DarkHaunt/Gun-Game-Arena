using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input
{
    public class JumpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<JumpParams, JumpEvent>> _filter;
        private readonly EcsPoolInject<JumpParams> _pool;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var entity = ref _pool.Value.Get(i);
                
                entity.Body.AddForce(Vector2.up * entity.Force);
            }
        }
    }
}