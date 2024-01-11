using Game.Scripts.Infrastructure.RootStateMachine.States;
using Game.Scripts.Infrastructure.RootStateMachine;
using Game.Scripts.Common.StateMachine;
using System.Collections.Generic;
using Zenject;
using System;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine : IInitializable
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _state;

        private readonly GameStateMachine _gameStateMachine;
        
        
        public MenuStateMachine(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        
        public async void Initialize()
        {
            await _gameStateMachine.Enter<MenuState>();
        }
    }
}
