using Game.Scripts.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Boot
{
    public class Bootstrapper : MonoBehaviour, IInitializable
    {
        private SceneLoader _sceneLoader;

        
        [Inject]
        public void Construct(SceneLoader sceneLoader)
            => _sceneLoader = sceneLoader;

        public void Initialize()
            => _sceneLoader.LoadScene(Scene.Menu);
    }
}