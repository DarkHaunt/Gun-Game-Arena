using Game.Scripts.Gameplay.Projectiles;
using Game.Scripts.Gameplay.Weapons.Shooting;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public class ShootingWeaponView : WeaponView
    {
        [Header("--- Shooting Data ---")]
        [SerializeField] private ProjectileConfig _projectile;
        [SerializeField] private float _cooldown;
        
        private EcsPool<ShootingData> _shootingDatas;


        protected override void ConstructInternal(EcsWorld world)
        {
            _shootingDatas = world.GetPool<ShootingData>();
        }

        protected override void PickUpHandleInternal(int weapon)
        {
            ref var shooting = ref _shootingDatas.Add(weapon);
            shooting.ProjectileConfig = _projectile;
            shooting.Cooldown = _cooldown;
        }
    }
}