using Game.Scripts.Gameplay.Entities.Attack;
using Game.Scripts.Gameplay.Player.Base;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player.Targeting
{
    public class TargetCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target, TargetCheckRequest, PlayerTag>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<Target>, Exc<PlayerTag>> _targetsFilter = default;
        
        private readonly EcsPoolInject<AttackRequest> _attackRequests = default;
        private readonly EcsPoolInject<Target> _targets = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var player = _targets.Value.Get(entity);

                var closest = _targetsFilter.Value.GetRawEntities()
                    .OrderBy(x => Vector3.Distance(_targets.Value.Get(x).Self.position, player.Self.position))
                    .First();
                
                ref var target = ref closest;

                _attackRequests.Value.Add(target);
            }
        }
    }
}