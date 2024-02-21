using Game.Scripts.Gameplay.Entities.Movement;
using Game.Scripts.Gameplay.Input.Events;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Gameplay.Input
{
    public class InputHandleSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsCustomInject<InputActions> _inputActions;

        private readonly EcsFilterInject<Inc<Move, InputListener>> _moveHandlers = default;
        private readonly EcsFilterInject<Inc<InputListener>> _listeners = default;
        
        private readonly EcsPoolInject<AttackEvent> _attackEvents = default;
        private readonly EcsPoolInject<Move> _movePool = default;


        public void Init(IEcsSystems systems)
        {
            _inputActions.Value.Enable();
            _inputActions.Value.Game.Attack.performed += AttackEventSend;
            _inputActions.Value.Game.Move.performed += UpdateMoveInput;
            _inputActions.Value.Game.Move.canceled += CancelMoveInput;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Value.Disable();
            _inputActions.Value.Game.Attack.performed -= AttackEventSend;
            _inputActions.Value.Game.Move.performed -= UpdateMoveInput;
            _inputActions.Value.Game.Move.canceled -= CancelMoveInput;
        }

        private void AttackEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listeners.Value)
                _attackEvents.Value.Add(i);
        }

        private void CancelMoveInput(InputAction.CallbackContext _)
            => SetInputDirection(Vector2.zero);

        private void UpdateMoveInput(InputAction.CallbackContext walkContext)
            => SetInputDirection(walkContext.ReadValue<Vector2>());

        private void SetInputDirection(Vector2 direction)
        {
            foreach (var i in _moveHandlers.Value)
            {
                ref var input = ref _movePool.Value.Get(i);
                input.Direction = direction;
            }
        }
    }
}