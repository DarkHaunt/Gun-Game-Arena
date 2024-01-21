using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Game.Scripts.Gameplay.Move.Grounding;
using Game.Scripts.Gameplay.Physic;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Jump
{
    public class JumpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Jump, Physical2D, Ground, JumpEvent>> _filter = default;
        
        private readonly EcsPoolInject<Physical2D> _physicPool = default;
        private readonly EcsPoolInject<Ground> _groundPool = default;
        private readonly EcsPoolInject<Jump> _jumpPool = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var ground = ref _groundPool.Value.Get(i);
                
                if(!ground.isGrounded)
                    return;
                    
                ref var entity = ref _jumpPool.Value.Get(i);
                ref var physic = ref _physicPool.Value.Get(i);
                
                physic.Body.AddForce(Vector2.up * entity.Force);
            }
        }
    }
}