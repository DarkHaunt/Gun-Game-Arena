using Game.Scripts.Common.StateMachine;
using Game.Scripts.Menu.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class JoinRoom : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly JoinRoomView _view;

        public JoinRoom(MenuStateMachine stateMachine, JoinRoomView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }

        public void Enter()
        {
            _view.Enable(true);
            _view.JoinButton.onClick.AddListener(JoinToRoom);
            _view.CancelButton.onClick.AddListener(SetMainMenuState);
        }

        public void Exit()
        {
            _view.Enable(false);
            _view.JoinButton.onClick.RemoveListener(JoinToRoom);
            _view.CancelButton.onClick.RemoveListener(SetMainMenuState);
        }

        private void JoinToRoom()
        {
            Debug.Log($"<color=white>{_view.GetRoomName()}</color>");
        }

        private void SetMainMenuState()
            => _stateMachine.Enter<MainMenu>();

        public class Factory : PlaceholderFactory<MenuStateMachine, JoinRoom> {}
    }
}