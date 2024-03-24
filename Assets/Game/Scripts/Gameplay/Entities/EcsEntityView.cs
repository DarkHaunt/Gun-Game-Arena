using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entities
{
    public class EcsEntityView : MonoBehaviour
    {
        private EcsPackedEntity _entity;

        public void Construct(EcsPackedEntity entity)
            => _entity = entity;

        public bool TryUnpackEntity(EcsWorld world, out int entity)
            => _entity.Unpack(world, out entity);
    }
}