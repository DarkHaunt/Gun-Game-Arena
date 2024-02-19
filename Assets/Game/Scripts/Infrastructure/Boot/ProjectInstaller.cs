using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Infrastructure.Scenes;
using Game.Scripts.Input;
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
            RegisterInputSystem();

            RegisterRootStateMachine();
            RegisterSceneLoadingComponents();

            Debug.Log($"<color=#76d1e3>ProjectInstaller Executed</color>");
        }

        private void RegisterInputSystem()
        {
            Container.Bind<InputActions>().AsTransient();
        }

        private void RegisterRootStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<BootState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LobbyState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();

            Container.BindFactory<BootState, BootState.Factory>().WhenInjectedInto<GameStateMachine>();
            Container.BindFactory<MenuState, MenuState.Factory>().WhenInjectedInto<GameStateMachine>();
            Container.BindFactory<LobbyState, LobbyState.Factory>().WhenInjectedInto<GameStateMachine>();
            Container.BindFactory<GameplayState, GameplayState.Factory>().WhenInjectedInto<GameStateMachine>();
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