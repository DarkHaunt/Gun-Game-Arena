using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class WeaponView : MonoBehaviour
    {
        [SerializeField] private float _duration;
        
        private EcsPool<WeaponSwitchRequest> _switchRequests;
        private EcsPool<WeaponHandleData> _handleDatas;
        
        private EcsWorld _world;

        public void Construct(EcsWorld world)
        {
            _world = world;
            
            _switchRequests = _world.GetPool<WeaponSwitchRequest>();
            _handleDatas = _world.GetPool<WeaponHandleData>();

            ConstructInternal(world);
        }

        public void HandlePickUp(EcsPackedEntity pickUpEntity)
        {
            if (!pickUpEntity.Unpack(_world, out int entity))
                throw new ArgumentException($"Can't unpack entity {pickUpEntity.Id} || {pickUpEntity.Gen}");

            var weapon = _world.NewEntity();
            
            ref var handle = ref _handleDatas.Add(weapon);
            handle.Duration = _duration;
            handle.WeaponEntity = _world.PackEntity(weapon);
            
            ref var switchRequest = ref _switchRequests.Add(entity);
            
            switchRequest.WeaponToSwitch = handle;
            switchRequest.Switcher = pickUpEntity;

            PickUpHandleInternal(weapon);

            Disable();
        }

        private void Disable()
            => gameObject.SetActive(false);


        protected abstract void ConstructInternal(EcsWorld world);
        protected abstract void PickUpHandleInternal(int weapon);
    }
}