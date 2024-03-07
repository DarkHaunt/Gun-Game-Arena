namespace Game.Scripts.Gameplay.Weapons
{
    public struct WeaponData
    {
        public WeaponType Type;
        
        public float HandledDuration;
        public float Duration;

        public void Copy(WeaponData source)
        {
            Type = source.Type;

            HandledDuration = source.HandledDuration;
            Duration = source.Duration;
        }
    }
}