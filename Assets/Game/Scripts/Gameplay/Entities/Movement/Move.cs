using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entities.Movement
{
    [Serializable]
    public struct Move
    {
        public Vector2 Direction;
        public float Speed;
    }
}