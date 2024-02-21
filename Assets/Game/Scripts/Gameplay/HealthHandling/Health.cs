namespace Game.Scripts.Gameplay.HealthHandling
{
    public struct Health
    {
        public float MaxHealth;
        public float Current;

        public void Init(float health)
            => Current = MaxHealth = health;
    }
}