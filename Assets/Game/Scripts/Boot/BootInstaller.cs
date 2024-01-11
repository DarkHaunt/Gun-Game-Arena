using UnityEngine;
using Zenject;

namespace Game.Scripts.Boot
{
    public class BootInstaller : MonoInstaller
    {
        [SerializeField] private Bootstrapper _bootstrapper;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Bootstrapper>()
                .FromInstance(_bootstrapper);
        }
    }
}