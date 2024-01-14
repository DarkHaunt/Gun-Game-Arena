using Game.Scripts.Gameplay.Input.Events;
using UnityEngine.InputSystem;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using static Game.Scripts.Gameplay.StaticData.GameplayStaticData;

namespace Game.Scripts.Gameplay.Input
{
    public class InputSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly InputActions _inputActions;
        
        private readonly EcsPoolInject<AttackEvent> _attackEventPool = EventWorld;
        private readonly EcsPoolInject<AttackEvent> _downEventPool = EventWorld;
        private readonly EcsPoolInject<AttackEvent> _jumpEventPool = EventWorld;
        private readonly EcsPoolInject<InputParams> _paramsPool = default;
        
        private readonly EcsFilterInject<Inc<InputListener>> _listenerFilter = default;
        private readonly EcsFilterInject<Inc<InputParams>> _paramsFilter;
        
        
        public InputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        
        public void Init(IEcsSystems systems)
        {
            _inputActions.Enable();
            
            _inputActions.Game.Jump.performed += SendJumpEvent;
            
            _inputActions.Game.Move.performed += UpdateMoveInput;
            _inputActions.Game.Move.canceled += CancelMoveInput;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Disable();
            
            _inputActions.Game.Jump.performed -= SendJumpEvent;
            
            _inputActions.Game.Move.performed -= UpdateMoveInput;
            _inputActions.Game.Move.canceled -= CancelMoveInput;
        }

        private void SendJumpEvent(InputAction.CallbackContext _)
        {
            foreach (var i in _listenerFilter.Value)
                _jumpEventPool.Value.Add(i);
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