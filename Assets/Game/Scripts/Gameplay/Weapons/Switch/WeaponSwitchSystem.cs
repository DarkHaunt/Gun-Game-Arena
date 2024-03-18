using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponSwitchSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSwitchRequest>> _requestsFilter = default;
        private readonly EcsPoolInject<WeaponSwitchRequest> _requestsPool = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _requestsFilter.Value)
            {
                ref var switchRequest = ref _requestsPool.Value.Get(entity);
                switchRequest.Switcher.CurrentWeapon = switchRequest.WeaponToSwitch;
            }
        }
    }
}