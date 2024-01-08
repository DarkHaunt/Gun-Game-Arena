using Game.Scripts.Common.StateMachine;
using System.Collections.Generic;
using System;

namespace Game.Scripts.Infrastructure.RootStateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        

        public void Enter<TState>() where TState : class, IState
        {
            var newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = _states[typeof(TState)] as TState;
            _activeState = state;
            
            return state;
        }
    }
}