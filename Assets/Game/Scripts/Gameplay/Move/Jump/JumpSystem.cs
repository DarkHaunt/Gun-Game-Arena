using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Jump
{
    public class JumpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Jump, JumpEvent>> _filter = default;
        private readonly EcsPoolInject<Jump> _pool = default;

        
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