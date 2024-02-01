using Game.Scripts.Common.StateMachine;
using Game.Scripts.Menu.UI;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class MainMenu : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly MenuView _view;
        

        public MainMenu(MenuStateMachine stateMachine, MenuView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }
        
        
        public void Enter()
        {
            _view.Enable(true);
            _view.CreateButton.onClick.AddListener(SetCreateRoomState);
            _view.JoinButton.onClick.AddListener(SetJoinRoomState);
            _view.ExitButton.onClick.AddListener(SetExitGameState);
        }

        public void Exit()
        {
            _view.Enable(false);
            _view.CreateButton.onClick.RemoveListener(SetCreateRoomState);
            _view.JoinButton.onClick.RemoveListener(SetJoinRoomState);
            _view.ExitButton.onClick.RemoveListener(SetExitGameState);
        }
        
        private void SetCreateRoomState()
            => _stateMachine.Enter<CreateRoom>();

        private void SetJoinRoomState()
            => _stateMachine.Enter<JoinRoom>();

        private void SetExitGameState()
            => _stateMachine.Enter<ExitGame>();
        
        public class Factory : PlaceholderFactory<MenuStateMachine, MainMenu> {}
    }
}