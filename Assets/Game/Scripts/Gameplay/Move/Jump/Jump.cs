using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Jump
{
    [Serializable]
    public struct Jump
    {
        public Rigidbody2D Body;
        public float Force;
    }
}