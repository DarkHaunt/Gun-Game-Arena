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
            _view.StartButton.onClick.AddListener(SetStartGameState);
            _view.ExitButton.onClick.AddListener(SetExitGameState);
        }

        public void Exit()
        {
            _view.Enable(false);
            _view.StartButton.onClick.RemoveListener(SetStartGameState);
            _view.ExitButton.onClick.RemoveListener(SetExitGameState);
        }
        
        private void SetStartGameState()
            => _stateMachine.Enter<StartGame>();

        private void SetExitGameState()
            => _stateMachine.Enter<ExitGame>();
        
        public class Factory : PlaceholderFactory<MenuStateMachine, MainMenu> {}
    }
}