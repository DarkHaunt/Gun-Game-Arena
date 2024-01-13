using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input
{
    public class JumpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<JumpParams> _pool;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpParams>()
                .Inc<JumpEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _pool.Get(i);
                
                entity.Body.AddForce(Vector2.up * entity.Force);
            }
        }
    }
}