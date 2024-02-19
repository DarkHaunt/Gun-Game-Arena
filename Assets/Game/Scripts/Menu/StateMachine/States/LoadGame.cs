using Game.Scripts.Infrastructure.Scenes;
using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class LoadGame : IState
    {
        private readonly SceneLoader _sceneLoader;

        public LoadGame(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadScene(Scene.Game);
        }

        public void Exit() { }
        
        public class Factory : PlaceholderFactory<LoadGame> {}
    }
}