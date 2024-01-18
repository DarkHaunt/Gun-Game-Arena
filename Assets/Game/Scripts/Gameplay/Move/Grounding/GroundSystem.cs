using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Extensions;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using LeoEcsPhysics;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Grounding
{
    public class GroundSystem : IEcsRunSystem
    {
        /*private readonly EcsFilterInject<Inc<Grounding, OnTriggerExit2DEvent>> _exitFilter = default;
        private readonly EcsPoolInject<OnTriggerExit2DEvent> _exitPool = default;*/

        private readonly EcsFilterInject<Inc<OnTriggerEnter2DEvent>> _enterFilter = default;
        private readonly EcsPoolInject<OnTriggerEnter2DEvent> _enterPool = default;
        
        private readonly EcsPoolInject<Grounding> _groundPool = default;

        private readonly EcsPackedEntityWithWorld _packedEntity;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _enterFilter.Value)
            {
                Debug.Log($"<color=white>Enter - {i}</color>");
               // ref var entity = ref _groundPool.Value.Get(i);
                ref var e = ref _enterPool.Value.Get(i);

                /*if (entity.groundLayer.ContainsLayer(e.collider2D.gameObject.layer))
                {
                    Debug.Log($"<color=white>Enter</color>");
                    entity.isGrounded = true;
                }*/
            }

            /*foreach (var i in _exitFilter.Value)
            {
                ref var entity =  ref _groundPool.Value.Get(i);
                ref var e = ref _exitPool.Value.Get(i);

                if (entity.groundLayer.ContainsLayer(e.collider2D.gameObject.layer))
                {
                    entity.isGrounded = false;
                    Debug.Log($"<color=white>Exit</color>");
                }
            }*/
        }
    }
}