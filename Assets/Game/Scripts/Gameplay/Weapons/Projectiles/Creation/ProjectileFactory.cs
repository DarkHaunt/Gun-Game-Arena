using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Entities.Physics;
using Game.Scripts.Infrastructure.Assets;
using Game.Scripts.Gameplay.StaticData;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Projectiles.Creation
{
    public class ProjectileFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly EcsWorld _world;

        private readonly EcsPool<ProjectileData> _projectileData;
        private readonly EcsPool<Physical2D> _physicalPool;
        private readonly EcsPool<Move> _movePool;
        
        
        public ProjectileFactory(EcsWorld world, AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _world = world;
        }

        
        public void CreateProjectile(Vector3 pos, Vector3 direction, ProjectileConfig config)
        {
            var prefab = _assetProvider.GetSync<ProjectileView>(PathIndents.ProjectileViewPath);
            var view = Object.Instantiate(prefab, pos, Quaternion.identity);

            var projectile = _world.NewEntity();

            ref var physics = ref _physicalPool.Add(projectile);
            physics.Body = view.Rigidbody;
            
            ref var move = ref _movePool.Add(projectile);
            move.Direction = direction;
            move.Speed = config.Speed;

            ref var data = ref _projectileData.Add(projectile);
            data.Damage = config.Damage;
            
            view.Construct(_world.PackEntityWithWorld(projectile));
        }
    }
}