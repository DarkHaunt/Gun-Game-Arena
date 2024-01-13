using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Input
{
    [Serializable]
    public struct JumpParams
    {
        public Rigidbody2D Body;
        public Vector2 Direction;
        public float Force;
    }
}