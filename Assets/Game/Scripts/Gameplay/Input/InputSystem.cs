using Game.Scripts.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input
{
    public class InputSystem : IEcsPreInitSystem, IEcsDestroySystem
    {
        private readonly InputActions _inputActions;
        
        private readonly EcsFilter _filter;
        private readonly EcsWorld _world;
        
        
        public InputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
            _inputActions.Enable();
        }
        
        
        public void PreInit(EcsSystems systems)
        {
            Debug.Log($"<color=white>Input Activate</color>");
            _inputActions.Enable();
        }

        public void Destroy(EcsSystems systems)
            => _inputActions.Disable();
    }
}