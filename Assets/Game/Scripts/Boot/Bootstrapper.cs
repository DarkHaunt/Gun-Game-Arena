using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Boot
{
    public class Bootstrapper : MonoBehaviour, IInitializable
    {
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;


        [Inject]
        public void Construct(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Initialize()
        {
            await _gameStateMachine.Enter<BootState>();
            
            await _sceneLoader.LoadScene(Scene.Menu);
        }
    }
}