using Game.Scripts.Extensions;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Cameras
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraFollow>> _filter = default;
        private readonly EcsPoolInject<CameraFollow> _pool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var follow = ref _pool.Value.Get(entity);
                follow.Self.SetInPosition(follow.TargetPos + follow.Offset);
            }
        }
    }
}