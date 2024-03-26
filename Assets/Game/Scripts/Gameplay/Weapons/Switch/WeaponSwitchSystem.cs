using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Weapons
{
    public class WeaponSwitchSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSwitchRequest>> _requestsFilter;

        private readonly EcsPoolInject<WeaponSwitchRequest> _requestsPool;
        private readonly EcsPoolInject<WeaponHandler> _handlers;

        private readonly EcsWorldInject _world;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _requestsFilter.Value)
            {
                var world = _world.Value;

                ref var switchRequest = ref _requestsPool.Value.Get(entity);

                if (switchRequest.Switcher.Unpack(world, out int i))
                {
                    ref var handler = ref _handlers.Value.Get(i);
                    handler.CurrentHandleData = switchRequest.WeaponToSwitch;
                }
            }
        }
    }
}