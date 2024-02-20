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

        private readonly EcsFilterInject<Inc<InputEventsListener>> _listeners = default;
        private readonly EcsCustomInject<InputActions> _inputActions;


        public void Init(IEcsSystems systems)
        {
            _inputActions.Value.Enable();
            _inputActions.Value.Game.Attack.performed += AttackEventSend;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputActions.Value.Disable();
            _inputActions.Value.Game.Attack.performed -= AttackEventSend;
        }

        private void AttackEventSend(InputAction.CallbackContext _)
        {
            foreach (var i in _listeners.Value)
                _attackEvents.Value.Add(i);
        }
    }
}