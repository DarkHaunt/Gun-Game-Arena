using Game.Scripts.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Boot
{
    public class ProjectInstaller : MonoInstaller
    {
        [Header("--- Parent For Don't Destroy ---")]
        [SerializeField] private Transform _parent;
        
        [Header("--- Scene Loading ---")]
        [SerializeField] private SceneTransitionHandler _sceneTransitionHandler;
        
        
        public override void InstallBindings()
        {
            RegisterSceneLoadingComponents();

            Debug.Log($"<color=#76d1e3>ProjectInstaller Executed</color>");
        }

        private void RegisterSceneLoadingComponents()
        {
            Container
                .Bind<SceneTransitionHandler>()
                .FromComponentInNewPrefab(_sceneTransitionHandler)
                .UnderTransform(_parent)
                .AsSingle();
            
            Container.Bind<SceneLoader>().AsSingle();
        }
    }
}