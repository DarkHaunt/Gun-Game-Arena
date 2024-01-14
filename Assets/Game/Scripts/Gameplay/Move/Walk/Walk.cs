using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Walk
{
    [Serializable]
    public struct Walk
    {
        public Rigidbody2D Body;
        public float Speed;
    }
}