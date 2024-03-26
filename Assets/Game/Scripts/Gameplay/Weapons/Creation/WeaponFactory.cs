using Game.Scripts.Gameplay.Weapons.Configs;
using Game.Scripts.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Creation
{
    public class WeaponFactory
    {
        private readonly AvailableWeaponsConfig _config;
        private readonly EcsWorld _world;

        private readonly EcsPool<WeaponHandleData> _dataPool;

        public WeaponFactory(EcsWorld world, AvailableWeaponsConfig config)
        {
            _config = config;
            _world = world;

            _dataPool = _world.GetPool<WeaponHandleData>();
        }

        public WeaponView CreateRandomWeapon()
        {
            var weapon = _config.AvailableWeapons.PickRandom();

            return CreateWeapon(weapon);
        }

        public void CreateDefaultWeapon(out WeaponHandleData weapon)
        {
            var entity = _world.NewEntity();
            
            weapon = _dataPool.Add(entity);
            weapon.Duration = float.PositiveInfinity;
        }

        private WeaponView CreateWeapon(WeaponView prefab)
        {
            var weapon = Object.Instantiate(prefab, Vector3.one * 5f, Quaternion.identity); // TODO: Test, change of course
            weapon.Construct(_world);

            return weapon;
        }
    }
}