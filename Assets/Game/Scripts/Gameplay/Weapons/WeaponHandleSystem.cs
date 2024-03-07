using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Time;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite; 

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponHandleSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WeaponData>> _weaponDataFilter = default;
        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;
        
        private readonly EcsCustomInject<TimeService> _timeService;
        private WeaponData _defaultWeaponData;
        
        public void Init(IEcsSystems systems)
            => _defaultWeaponData = Indents.Player.DefaultWeapon;
 
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _weaponDataFilter.Value)
            {
                ref var weapon = ref _weaponDataPool.Value.Get(entity);
                weapon.HandledDuration += _timeService.Value.DeltaTime;

                if (weapon.HandledDuration >= weapon.Duration)
                    weapon.Copy(_defaultWeaponData);
            }
        }
    }
}