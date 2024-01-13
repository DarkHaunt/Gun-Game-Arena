using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Moving
{
    [Serializable]
    public struct WalkParams
    {
        public Rigidbody2D Body;
        public float Speed;
    }
}