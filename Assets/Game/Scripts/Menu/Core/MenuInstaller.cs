using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Menu.StateMachine;
using Game.Scripts.Menu.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Boot
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("--- Views ---")]
        [SerializeField] private MenuView _menuView;
        [SerializeField] private JoinRoomView _joinRoomView;
        [SerializeField] private CreateRoomView _createRoomView;
        
        
        public override void InstallBindings()
        {
            RegisterStateMachine();
            RegisterViews();
        }

        private void RegisterStateMachine()
        {
            Container.BindInterfacesAndSelfTo<MenuStateMachine>().AsSingle();

            Container.Bind<MainMenu>().AsSingle();
            Container.Bind<CreateRoom>().AsSingle();
            Container.Bind<JoinRoom>().AsSingle();
            Container.Bind<LoadLobby>().AsSingle();
            Container.Bind<ExitGame>().AsSingle();

            Container.BindFactory<MenuStateMachine, MainMenu, MainMenu.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<MenuStateMachine, CreateRoom, CreateRoom.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<MenuStateMachine, JoinRoom, JoinRoom.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<LoadLobby, LoadLobby.Factory>().WhenInjectedInto<MenuStateMachine>();
            Container.BindFactory<ExitGame, ExitGame.Factory>().WhenInjectedInto<MenuStateMachine>();
        }

        private void RegisterViews()
        {
            Container.BindInstance(_menuView);
            Container.BindInstance(_joinRoomView);
            Container.BindInstance(_createRoomView);
        }
    }
}