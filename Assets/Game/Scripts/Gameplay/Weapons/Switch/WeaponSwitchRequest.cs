using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Weapons
{
    public struct WeaponSwitchRequest
    {
        public WeaponHandleData WeaponToSwitch;
        public EcsPackedEntity Switcher;
    }
}