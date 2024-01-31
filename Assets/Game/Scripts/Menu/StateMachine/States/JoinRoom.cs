using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class JoinRoom : IState
    {
        public void Exit()
        {
        }

        public void Enter()
        {
        }
        
        public class Factory : PlaceholderFactory<JoinRoom> {}
    }
}