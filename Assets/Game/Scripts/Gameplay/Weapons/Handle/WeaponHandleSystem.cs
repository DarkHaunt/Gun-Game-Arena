using Game.Scripts.Gameplay.Weapons.Creation;
using Game.Scripts.Gameplay.Time;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponHandleSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<WeaponFactory> _weaponFactory;
        private readonly EcsCustomInject<TimeService> _timeService;

        private readonly EcsFilterInject<Inc<WeaponHandler>> _filter;
        private readonly EcsWorldInject _world;

        private readonly EcsPoolInject<WeaponSwitchRequest> _switchRequests;
        private readonly EcsPoolInject<WeaponHandler> _handlers;

        private WeaponHandleData _defaultWeaponHandleData;


        public void Init(IEcsSystems systems)
            => _weaponFactory.Value.CreateDefaultWeapon(out _defaultWeaponHandleData);

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var handler = ref _handlers.Value.Get(entity);
                ref var handleData = ref handler.CurrentHandleData;

                handleData.HandledDuration += _timeService.Value.DeltaTime;

                if (handleData.HandledDuration >= handleData.Duration)
                {
                    if (handleData.WeaponEntity.Unpack(_world.Value, out var weapon))
                        _world.Value.DelEntity(weapon);
                    
                    SwitchToDefaultWeapon(entity);
                }
            }
        }

        private void SwitchToDefaultWeapon(int entity)
        {
            ref var request = ref _switchRequests.Value.Add(entity);
            request.WeaponToSwitch = _defaultWeaponHandleData;
            request.Switcher = _world.Value.PackEntity(entity);
        }
    }
}