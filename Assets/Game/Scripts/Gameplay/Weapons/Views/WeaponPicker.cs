using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponPicker : MonoBehaviour
    {
        private EcsPackedEntity _entity;

        public void Construct(EcsPackedEntity entity)
            => _entity = entity;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WeaponView weaponView))
                weaponView.HandlePickUp(_entity);
        }
    }
}