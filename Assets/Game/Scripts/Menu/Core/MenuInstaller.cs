using Game.Scripts.Menu.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.Boot
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MenuStateMachine>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<MenuController>()
                .AsSingle();
        }
    }
}