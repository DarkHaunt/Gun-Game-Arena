using Game.Scripts.Gameplay.Input;
using Game.Scripts.Gameplay.Input.Events;
using Game.Scripts.Gameplay.Moving;
using Leopotam.EcsLite;
using Zenject;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEcsCore();
            
            BindSystems();
        }

        private void BindEcsCore()
        {
            Container.BindInterfacesTo<GameplayEscHandler>().AsSingle();
            Container.Bind<EcsWorld>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<EventsCleanUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<WalkSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<JumpSystem>().AsSingle();
        }
    }
}