using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Moving
{
    [Serializable]
    public struct Move
    {
        public Vector2 Direction;
        public float Speed;
    }
}