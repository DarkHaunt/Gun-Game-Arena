using Game.Scripts.Gameplay.Input.Event_Handling;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Jump;
using Game.Scripts.Gameplay.Move.Walk;
using Leopotam.EcsLite;
using Zenject;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEcsCore();
            
            BindInputSystems();
            BindMoveSystems();
        }

        private void BindEcsCore()
        {
            Container.BindInterfacesTo<GameplayEscHandler>().AsSingle();
            Container.Bind<EcsWorld>().AsSingle();
        }

        private void BindInputSystems()
        {
            Container.BindInterfacesAndSelfTo<InputEventsSendSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputMoveHandleSystem>().AsSingle();
        }

        private void BindMoveSystems()
        {
            Container.BindInterfacesAndSelfTo<WalkSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<JumpSystem>().AsSingle();
        }
    }
}