using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Moving
{
    public class MoveSystem : IEcsPreInitSystem
    {
        public void PreInit(EcsSystems systems)
        {
            Debug.Log($"<color=white>Move Activate</color>");
            
        }
    }
}