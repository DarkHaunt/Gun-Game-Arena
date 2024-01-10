using Game.Scripts.Menu.StateMachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Boot
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log($"<color=yellow>Log</color>");

            Container
                .BindInterfacesAndSelfTo<MenuStateMachine>()
                .AsSingle();
        }
    }
}