using Game.Scripts.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Grounding
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundChecker : MonoBehaviour
    {
        private EcsPackedEntity _owner;
        private EcsPool<Ground> _pool;


        public void Init(EcsPackedEntity owner, EcsPool<Ground> pool)
        {
            _pool = pool;
            _owner = owner;
        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            ref var ground = ref _pool.Get(_owner.Id);
            
            if (IsGroundLayerDetectedOn(other, ground))
                ground.isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ref var ground = ref _pool.Get(_owner.Id);

            if (IsGroundLayerDetectedOn(other, ground))
                ground.isGrounded = false;
        }

        private static bool IsGroundLayerDetectedOn(Component other, Ground ground)
            => ground.groundLayer.ContainsLayer(other.gameObject.layer);
    }
}