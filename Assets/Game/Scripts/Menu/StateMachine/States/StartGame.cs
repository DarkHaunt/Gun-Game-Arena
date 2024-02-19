using Game.Scripts.Common.StateMachine;
using Game.Scripts.Menu.UI;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly StartGameView _view;

        public StartGame(MenuStateMachine stateMachine, StartGameView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }

        public void Enter()
        {
            _view.Enable(true);
            _view.StartButton.onClick.AddListener(SetLoadGame);
            _view.CancelButton.onClick.AddListener(SetMainMenuState);
        }

        public void Exit()
        {
            _view.Enable(false);
            _view.StartButton.onClick.RemoveListener(SetLoadGame);
            _view.CancelButton.onClick.RemoveListener(SetMainMenuState);
        }

        private void SetLoadGame()
            => _stateMachine.Enter<LoadGame>();

        private void SetMainMenuState()
            => _stateMachine.Enter<MainMenu>();

        public class Factory : PlaceholderFactory<MenuStateMachine, StartGame> {}
    }
}