using System;
using System.Collections.Generic;
using Game.Scripts.Common.StateMachine;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _state;
        
        public MenuStateMachine()
        {
            
        }
    }
}
