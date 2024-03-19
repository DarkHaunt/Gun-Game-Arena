using Game.Scripts.Gameplay.Environment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentData _data;
        
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayBootstrapper>()
                .AsSingle()
                .WithArguments(_data);
        }
    }
}