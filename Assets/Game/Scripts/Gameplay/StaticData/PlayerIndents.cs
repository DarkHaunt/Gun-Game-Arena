using Game.Scripts.Gameplay.Weapons;

namespace Game.Scripts.Gameplay.StaticData
{
    public static class PlayerIndents
    {
        public static readonly WeaponHandleData DefaultWeapon = new()
        {
            Duration = float.PositiveInfinity,
        };
    }
}