using System;
using Zenject;
using System.Collections.Generic;
using Game.Scripts.Common.StateMachine;
using UnityEngine;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine : IInitializable
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _state;
        
        public void Initialize()
        {
            Debug.Log($"<color=white>Init Menu </color>");
        }
    }
}
