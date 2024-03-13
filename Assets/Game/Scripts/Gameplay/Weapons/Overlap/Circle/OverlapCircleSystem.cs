using Game.Scripts.Gameplay.Entities;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Overlap
{
    public class OverlapCircleSystem : IEcsRunSystem
    {
        private readonly Collider2D[] _targets = new Collider2D[50]; // Should be enough
        
        private readonly EcsFilterInject<Inc<OverlapCircleRequest>> _filter = default;
        
        private readonly EcsPoolInject<OverlapCircleRequest> _requests = default;
        private readonly EcsPoolInject<OverlappedTag> _overlapped = default;

        private readonly EcsWorldInject _world = default;

        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var request = ref _requests.Value.Get(i);

                var count = Physics2D.OverlapCircleNonAlloc(request.Position, request.Radius, _targets);

                for (int j = 0; j < count; j++)
                {
                    if (TryToGetEntity(i, out var entity))
                        _overlapped.Value.Add(entity);
                }
            }
        }

        private bool TryToGetEntity(int i, out int entity)
        {
            entity = int.MaxValue;
            
            return _targets[i].TryGetComponent(out EcsEntityView view) 
                   && view.Entity.Unpack(_world.Value, out entity);
        }
    }
}