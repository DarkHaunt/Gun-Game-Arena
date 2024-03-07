using Game.Scripts.Infrastructure.RootStateMachine.States;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

namespace Game.Scripts.Infrastructure.RootStateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IAsyncState> _states;
        private IAsyncState _activeState;

        
        public GameStateMachine(BootState.Factory bootFactory, MenuState.Factory menuFactory, GameplayState.Factory gameFactory)
        {
            _states = new Dictionary<Type, IAsyncState>(3)
            {
                [typeof(BootState)] = bootFactory.Create(),
                [typeof(MenuState)] = menuFactory.Create(),
                [typeof(GameplayState)] = gameFactory.Create()
            };
        }
        

        public async UniTask Enter<TState>() where TState : class, IAsyncState
        {
            _activeState?.Exit();
            
            _activeState = _states[typeof(TState)] as TState;
            
            await _activeState!.Enter();
        }
    }
}