using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayBootstrapper>()
                .AsSingle()
                .WithArguments(_camera);
        }
    }
}