using Game.Scripts.Gameplay.Moving;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplayEscHandler>().AsSingle();

            Container.BindInterfacesTo<CharacterMoveSystem>().AsSingle();
        }
    }
}