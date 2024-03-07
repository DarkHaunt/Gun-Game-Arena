using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public abstract class WeaponView : MonoBehaviour
    {
        private EcsWorld _world;

        public void Construct(EcsWorld world)
        {
            _world = world;
        }
        
        public abstract void PickUpHandle(EcsPackedEntity pickUpEntity);
    }
}