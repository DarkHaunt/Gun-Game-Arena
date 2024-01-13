using Game.Scripts.Gameplay.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Moving
{
    public class WalkSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<InputParams> _inputPool;
        private EcsPool<WalkParams> _walkPool;
        
        private EcsFilter _filter;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _inputPool = world.GetPool<InputParams>();
            _walkPool = world.GetPool<WalkParams>();
            
            _filter = world
                .Filter<WalkParams>()
                .Inc<InputParams>()
                .End();
            
            Debug.Log($"<color=white>Move Activate</color>");
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var walkParams = ref _walkPool.Get(entity);
                ref var inputParams = ref _inputPool.Get(entity);

                walkParams.Body.velocity = new Vector2(inputParams.XDirection * walkParams.Speed, 0f);
            }
        }
    }
}