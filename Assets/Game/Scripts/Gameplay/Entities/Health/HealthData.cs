namespace Game.Scripts.Gameplay.HealthHandling
{
    public struct HealthData
    {
        public float MaxHealth;
        public float Current;

        public void Init(float health)
            => Current = MaxHealth = health;
    }
}