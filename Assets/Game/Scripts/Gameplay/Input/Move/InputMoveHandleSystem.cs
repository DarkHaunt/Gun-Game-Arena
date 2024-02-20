using UnityEngine.InputSystem;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input.Move
{
    public class InputMoveHandleSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<InputActions> _inputActions;

        private readonly EcsFilterInject<Inc<InputMove>> _filter = default;
        private readonly EcsPoolInject<InputMove> _pool = default;


        public void Init(IEcsSystems systems)
        {
            _inputActions.Value.Enable();
            _inputActions.Value.Game.Move.performed += UpdateMoveInput;
            _inputActions.Value.Game.Move.canceled += CancelMoveInput;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Value.Disable();
            _inputActions.Value.Game.Move.performed -= UpdateMoveInput;
            _inputActions.Value.Game.Move.canceled -= CancelMoveInput;
        }

        private void CancelMoveInput(InputAction.CallbackContext _)
            => SetInputDirection(Vector2.zero);

        private void UpdateMoveInput(InputAction.CallbackContext walkContext)
            => SetInputDirection(walkContext.ReadValue<Vector2>());

        private void SetInputDirection(Vector2 direction)
        {
            foreach (var i in _filter.Value)
            {
                ref var input = ref _pool.Value.Get(i);
                input.Direction = direction;
            }
        }
    }
}