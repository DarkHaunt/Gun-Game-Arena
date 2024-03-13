using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entities
{
    public class EcsEntityView : MonoBehaviour
    {
        public EcsPackedEntity Entity { get; private set;  }

        public void Construct(EcsPackedEntity entity)
            => Entity = entity;
    }
}