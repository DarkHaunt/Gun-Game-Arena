using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Common.StateMachine;
using System.Collections.Generic;
using System;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _state;

        public MenuStateMachine(CreateRoom.Factory createRoomFactory, JoinRoom.Factory joinRoomFactory, 
            MainMenu.Factory mainMenuFactory, ExitGame.Factory exitGameFactory)
        {
            _states = new Dictionary<Type, IState>(4)
            {
                [typeof(MainMenu)] = mainMenuFactory.Create(),
                [typeof(CreateRoom)] = createRoomFactory.Create(),
                [typeof(JoinRoom)] = joinRoomFactory.Create(),
                [typeof(ExitGame)] = exitGameFactory.Create()
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            _state?.Exit();
            _state = _states[typeof(TState)] as TState;
            _state!.Enter();
        }
    }
}
