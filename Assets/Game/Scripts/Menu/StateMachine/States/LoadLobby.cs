using Game.Scripts.Infrastructure.Scenes;
using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class LoadLobby : IState
    {
        private readonly SceneLoader _sceneLoader;

        public LoadLobby(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadScene(Scene.Lobby);
        }

        public void Exit() { }
        
        public class Factory : PlaceholderFactory<LoadLobby> {}
    }
}