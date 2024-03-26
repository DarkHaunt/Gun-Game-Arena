using Game.Scripts.Gameplay.Projectiles;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Shooting
{
    public struct ShootingData
    {
        public ProjectileConfig ProjectileConfig;
        public Transform ShootPoint;
        public float Cooldown;
    }
}