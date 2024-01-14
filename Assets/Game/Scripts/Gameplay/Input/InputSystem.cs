using Game.Scripts.Gameplay.Input.Events;
using UnityEngine.InputSystem;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;


namespace Game.Scripts.Gameplay.Input
{
    public class InputSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly InputActions _inputActions;

        private readonly EcsPoolInject<AttackEvent> _attackEventPool = default;
        private readonly EcsPoolInject<AttackEvent> _downEventPool = default;
        private readonly EcsPoolInject<JumpEvent> _jumpEventPool = default;
        private readonly EcsPoolInject<InputParams> _paramsPool = default;

        private readonly EcsFilterInject<Inc<InputListener>> _listenerFilter = default;
        private readonly EcsFilterInject<Inc<InputParams>> _paramsFilter = default;


        public InputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }


        public void Init(IEcsSystems systems)
        {
            _inputActions.Enable();

            _inputActions.Game.Attack.performed += AttackEventSend;
            _inputActions.Game.Down.performed += DownEventSend;
            _inputActions.Game.Jump.performed += JumpEventSend;

            _inputActions.Game.Move.performed += UpdateMoveInput;
            _inputActions.Game.Move.canceled += CancelMoveInput;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Disable();

            _inputActions.Game.Attack.performed -= AttackEventSend;
            _inputActions.Game.Down.performed -= DownEventSend;
            _inputActions.Game.Jump.performed -= JumpEventSend;

            _inputActions.Game.Move.performed -= UpdateMoveInput;
            _inputActions.Game.Move.canceled -= CancelMoveInput;
        }

        private void JumpEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listenerFilter.Value)
                _jumpEventPool.Value.Add(i);
        }     
        
        private void DownEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listenerFilter.Value)
                _downEventPool.Value.Add(i);
        }     
        
        private void AttackEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listenerFilter.Value)
                _attackEventPool.Value.Add(i);
        }

        private void CancelMoveInput(InputAction.CallbackContext _)
            => SetInputDirection(0f);

        private void UpdateMoveInput(InputAction.CallbackContext walkContext)
            => SetInputDirection(walkContext.ReadValue<float>());

        private void SetInputDirection(float direction)
        {
            foreach (var i in _paramsFilter.Value)
            {
                ref var input = ref _paramsPool.Value.Get(i);
                input.XDirection = direction;
            }
        }
    }
}