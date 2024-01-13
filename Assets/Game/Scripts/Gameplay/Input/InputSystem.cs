using UnityEngine.InputSystem;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input
{
    public class InputSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly InputActions _inputActions;
        
        private EcsPool<InputParams> _pool;
        private EcsFilter _filter;
        
        
        public InputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            _pool = world.GetPool<InputParams>();
            
            _filter = world
                .Filter<InputParams>()
                .End();
            
            _inputActions.Enable();
            
            _inputActions.Game.Move.performed += UpdateInput;
            _inputActions.Game.Move.canceled += CancelInput;
        }

        public void Destroy(EcsSystems systems)
        {
            _inputActions.Disable();
            
            _inputActions.Game.Move.performed -= UpdateInput;
            _inputActions.Game.Move.canceled -= CancelInput;
        }

        private void CancelInput(InputAction.CallbackContext _)
            => SetInputDirection(Vector2.zero);

        private void UpdateInput(InputAction.CallbackContext walkContext)
            => SetInputDirection(walkContext.ReadValue<Vector2>());

        private void SetInputDirection(Vector2 direction)
        {
            foreach (var i in _filter)
            {
                ref var input = ref _pool.Get(i);
                input.Direction = direction;
            }
        }
    }
}