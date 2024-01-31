using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class MainMenu : IState
    {
        public void Enter()
        {
            
        }

        public void Exit()
        {
        }
        
        public class Factory : PlaceholderFactory<MainMenu> {}
    }
}