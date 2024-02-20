using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input.Move
{
    [Serializable]
    public struct InputMove
    {
        [HideInInspector] public Vector2 Direction;
    }
}