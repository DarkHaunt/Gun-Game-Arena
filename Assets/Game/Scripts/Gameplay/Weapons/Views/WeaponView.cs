using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponHandleData _handleData;
        
        private EcsPool<WeaponSwitchRequest> _switchRequests;
        
        private EcsWorld _world;

        public void Construct(EcsWorld world)
        {
            _world = world;
            
            _switchRequests = _world.GetPool<WeaponSwitchRequest>();
        }

        public void HandlePickUp(EcsPackedEntity pickUpEntity)
        {
            if (!pickUpEntity.Unpack(_world, out int entity))
                throw new ArgumentException($"Can't unpack entity {pickUpEntity.Id} || {pickUpEntity.Gen}");

            ref var switchRequest = ref _switchRequests.Add(entity);
            
            switchRequest.WeaponToSwitch = _handleData;
            switchRequest.Switcher = pickUpEntity;

            PickUpHandleInternal(_world, entity);

            Disable();
        }

        private void Disable()
            => gameObject.SetActive(false);

        protected abstract void PickUpHandleInternal(EcsWorld pickUpEntity, int entity);
    }
}