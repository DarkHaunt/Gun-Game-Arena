using Game.Scripts.Gameplay.HealthHandling;

namespace Game.Scripts.Gameplay.Entities.Attack
{
    public struct AttackData
    {
        public bool IsOnDistance;
        public HealthData Target;
        
        public float Cooldown;
        public float Damage;
    }
}