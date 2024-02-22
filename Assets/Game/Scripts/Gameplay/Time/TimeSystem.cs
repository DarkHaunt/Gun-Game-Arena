using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Scripts.Gameplay.Time
{
    public class TimeSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            _timeService.Value.Time = UnityEngine.Time.time;
            _timeService.Value.DeltaTime = UnityEngine.Time.deltaTime;
        }
    }
}