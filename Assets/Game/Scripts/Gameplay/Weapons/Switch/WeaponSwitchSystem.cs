using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponSwitchSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSwitchRequest>> _requestsFilter = default;
        
        private readonly EcsPoolInject<WeaponSwitchRequest> _requestsPool = default;
        private readonly EcsPoolInject<WeaponHandler> _handlers = default;
        
        private readonly EcsWorldInject _world = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _requestsFilter.Value)
            {
                ref var switchRequest = ref _requestsPool.Value.Get(entity);

                if (switchRequest.Switcher.Unpack(_world.Value, out int i))
                {
                    ref var handler = ref _handlers.Value.Get(i);
                    handler.CurrentWeapon = switchRequest.WeaponToSwitch;
                }
            }
        }
    }
}