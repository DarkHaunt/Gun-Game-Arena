using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Time;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponHandleSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<TimeService> _timeService;

        private readonly EcsFilterInject<Inc<WeaponHandler>> _filter;
        private readonly EcsWorldInject _world;

        private readonly EcsPoolInject<WeaponSwitchRequest> _switchRequests;
        private readonly EcsPoolInject<WeaponHandler> _handlers;

        private WeaponHandleData _defaultWeaponHandleData;


        public void Init(IEcsSystems systems)
            => _defaultWeaponHandleData = PlayerIndents.DefaultWeapon;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var handler = ref _handlers.Value.Get(entity);
                ref var weapon = ref handler.CurrentWeapon;

                weapon.HandledDuration += _timeService.Value.DeltaTime;

                if (weapon.HandledDuration >= weapon.Duration)
                {
                    ref var request = ref _switchRequests.Value.Add(entity);
                    request.WeaponToSwitch = _defaultWeaponHandleData;
                    request.Switcher = _world.Value.PackEntity(entity);
                }
            }
        }
    }
}