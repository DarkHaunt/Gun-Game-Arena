using Game.Scripts.Gameplay.Entities.Damage;
using Game.Scripts.Gameplay.Entities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Projectiles
{
    public class ProjectileView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        
        private EcsPool<ProjectileData> _projectileDataPool;
        private EcsPool<DamageRequest> _damageRequestPool;
        
        private EcsWorld _world;
        private int _projectile;


        public void Construct(EcsPackedEntityWithWorld packedProjectile)
        {
            packedProjectile.Unpack(out _world, out _projectile);
            
            _projectileDataPool = _world.GetPool<ProjectileData>();
            _damageRequestPool = _world.GetPool<DamageRequest>();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out EcsEntityView view) && view.TryUnpackEntity(_world, out int entity))
            {
                var data = _projectileDataPool.Get(_projectile);
                
                ref var damage = ref _damageRequestPool.Add(entity);
                damage.Damage = data.Damage;
                
                _world.DelEntity(_projectile);
                
                Destroy(gameObject);
            }
        }
    }
}