using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Game.Scripts.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game.Scripts.Gameplay.StaticData.StaticData;

namespace Game.Scripts.Gameplay.Input.Event_Handling
{
    public class InputEventsSendSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsPoolInject<AttackEvent> _attackEvents = EventWorld;
        private readonly EcsPoolInject<DownEvent> _downEvents = EventWorld;
        private readonly EcsPoolInject<JumpEvent> _jumpEvents = EventWorld;
        
        private readonly EcsFilterInject<Inc<InputEventsListener>> _listeners = EventWorld;
        
        private readonly InputActions _inputActions;

        
        public InputEventsSendSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        
        public void Init(IEcsSystems systems)
        {
            _inputActions.Enable();
            _inputActions.Game.Attack.performed += AttackEventSend;
            _inputActions.Game.Down.performed += DownEventSend;
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
            Debug.Log($"<color=white>Entites - {_listeners.Value.GetEntitiesCount()}</color>");

            foreach (var i in _listeners.Value)
                _jumpEvents.Value.Add(i);
        }
    }
}