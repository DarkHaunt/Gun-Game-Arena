using Game.Scripts.Common.StateMachine;
using Game.Scripts.Infrastructure.Scenes;

namespace Game.Scripts.Infrastructure.RootStateMachine.States
{
    public class Loading : IPayloadedState<Scene>
    {
        public void Enter(Scene payload)
        {
        }

        public void Exit()
        {
        }
    }
}