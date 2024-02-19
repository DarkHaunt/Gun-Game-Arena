using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Menu.StateMachine;
using Game.Scripts.Menu.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Core
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("--- Views ---")]
        [SerializeField] private MenuView _menuView;
        [SerializeField] private StartGameView _startGameView;
        
        
        public override void InstallBindings()
        {
            RegisterStateMachine();
            RegisterViews();
        }

        private void RegisterStateMachine()
        {
            Container.BindInterfacesAndSelfTo<MenuStateMachine>().AsSingle();

            Container.Bind<MainMenu>().AsSingle();
            Container.Bind<StartGame>().AsSingle();
            Container.Bind<LoadGame>().AsSingle();
            Container.Bind<ExitGame>().AsSingle();

            Container.BindFactory<MenuStateMachine, MainMenu, MainMenu.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<MenuStateMachine, StartGame, StartGame.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<LoadGame, LoadGame.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<ExitGame, ExitGame.Factory>().WhenInjectedInto<MenuStateMachine>();
        }

        private void RegisterViews()
        {
            Container.BindInstance(_menuView);
            Container.BindInstance(_startGameView);
        }
    }
}