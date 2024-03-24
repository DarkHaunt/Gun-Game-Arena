using Game.Scripts.Gameplay.Projectiles;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Shooting
{
    public struct ShootingRequest
    {
        public ProjectileConfig ProjectileConfig;
        
        public Vector3 ShootPoint;
        public Vector3 Direction;
        public float Cooldown;
    }
}