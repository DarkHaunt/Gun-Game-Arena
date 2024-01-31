using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Common.StateMachine;
using System.Collections.Generic;
using System;
using Zenject;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Dictionary<Type, IState> _states;
        private IState _state;

        public MenuStateMachine(GameStateMachine gameStateMachine, CreateRoom.Factory createRoomFactory, JoinRoom.Factory joinRoomFactory, 
            MainMenu.Factory mainMenuFactory, ExitGame.Factory exitGameFactory)
        {
            _gameStateMachine = gameStateMachine;
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

        public async void Initialize()
        {
            await _gameStateMachine.Enter<MenuState>();
            
            Enter<MainMenu>();
        }
    }
}
