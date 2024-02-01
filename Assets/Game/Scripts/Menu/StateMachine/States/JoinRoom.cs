using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class JoinRoom : IState
    {
        private readonly MenuStateMachine _stateMachine;

        public JoinRoom(MenuStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
        }
        
        public class Factory : PlaceholderFactory<MenuStateMachine, JoinRoom> {}
    }
}