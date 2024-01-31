using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Menu.StateMachine;
using Game.Scripts.Menu.UI;
using Zenject;
using System;

namespace Game.Scripts.Menu.Boot
{
    public class MenuController : IInitializable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MenuStateMachine _menuStateMachine;
        private readonly MenuView _view;
        

        public MenuController(MenuView view, GameStateMachine gameStateMachine, MenuStateMachine menuStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _menuStateMachine = menuStateMachine;
            _view = view;
        }
        

        public async void Initialize()
        {
            await _gameStateMachine.Enter<MenuState>();
            _menuStateMachine.Enter<MainMenu>();
            
            _view.CreateButton.onClick.AddListener(SetCreateRoomState);
            _view.JoinButton.onClick.AddListener(SetJoinRoomState);
            _view.ExitButton.onClick.AddListener(SetExitGameState);
        }

        public void Dispose()
        {
            _view.CreateButton.onClick.RemoveListener(SetCreateRoomState);
            _view.JoinButton.onClick.RemoveListener(SetJoinRoomState);
            _view.ExitButton.onClick.RemoveListener(SetExitGameState);
        }

        private void SetCreateRoomState()
            => _menuStateMachine.Enter<CreateRoom>();

        private void SetJoinRoomState()
            => _menuStateMachine.Enter<JoinRoom>();

        private void SetExitGameState()
            => _menuStateMachine.Enter<ExitGame>();
    }
}