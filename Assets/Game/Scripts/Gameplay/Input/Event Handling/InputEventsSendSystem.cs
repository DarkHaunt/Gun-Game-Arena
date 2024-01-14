using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using UnityEngine.InputSystem;
using Leopotam.EcsLite.Di;
using Game.Scripts.Input;
using Leopotam.EcsLite;

namespace Game.Scripts.Gameplay.Input.Event_Handling
{
    public class InputEventsSendSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsPoolInject<AttackEvent> _attackEvents = default;
        private readonly EcsPoolInject<DownEvent> _downEvents = default;
        private readonly EcsPoolInject<JumpEvent> _jumpEvents = default;

        private readonly EcsFilterInject<Inc<InputEventsListener>> _listeners = default;

        private readonly InputActions _inputActions;


        public InputEventsSendSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }


        public void Init(IEcsSystems systems)
        {
            _inputActions.Enable();
            // _inputActions.Game.Attack.performed += AttackEventSend;
            //  _inputActions.Game.Down.performed += DownEventSend;
            _inputActions.Game.Jump.performed += JumpEventSend;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Disable();
            _inputActions.Game.Attack.performed -= AttackEventSend;
            _inputActions.Game.Down.performed -= DownEventSend;
            _inputActions.Game.Jump.performed -= JumpEventSend;
        }

        private void AttackEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listeners.Value)
                _attackEvents.Value.Add(i);
        }

        private void DownEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listeners.Value)
                _downEvents.Value.Add(i);
        }

        private void JumpEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listeners.Value)
                _jumpEvents.Value.Add(i);
        }
    }
}