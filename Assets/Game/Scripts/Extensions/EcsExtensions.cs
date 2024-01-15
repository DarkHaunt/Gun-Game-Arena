using Leopotam.EcsLite;

namespace Game.Scripts.Extensions
{
    public static class EcsExtensions
    {
        public static ref T SendEvent<T>(this EcsWorld world) where T : struct
        {
            var pool = world.GetPool<T>();
            var entity = world.NewEntity();
            return ref pool.Add(entity);
        }
    }
}