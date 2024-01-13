using Game.Scripts.Gameplay.Input.Events;
using Game.Scripts.Gameplay.Move.Down;
using UnityEngine.InputSystem;
using Game.Scripts.Input;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Input
{
    public class InputSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly InputActions _inputActions;
        
        private EcsPool<InputListener> _listeners;
        private EcsPool<InputParams> _paramsPool;
        
        private EcsPool<AttackEvent> _attackEventPool;
        private EcsPool<DownEvent> _downEventPool;
        private EcsPool<JumpEvent> _jumpEventPool;
        
        private EcsFilter _listenerFilter;
        private EcsFilter _paramsFilter;
        
        
        public InputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _listeners = world.GetPool<InputListener>();
            _paramsPool = world.GetPool<InputParams>();

            _listenerFilter = world
                .Filter<InputListener>()
                .End();
            
            _paramsFilter = world
                .Filter<InputParams>()
                .End();
            
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

        public void SendJumpEvent(InputAction.CallbackContext _)
        {
            foreach (var i in _listenerFilter)
                _jumpEventPool.Add(i);
        }

        private void CancelMoveInput(InputAction.CallbackContext _)
            => SetInputDirection(0f);

        private void UpdateMoveInput(InputAction.CallbackContext walkContext)
            => SetInputDirection(walkContext.ReadValue<float>());

        private void SetInputDirection(float direction)
        {
            foreach (var i in _paramsFilter)
            {
                ref var input = ref _paramsPool.Get(i);
                input.XDirection = direction;
            }
        }
    }
}