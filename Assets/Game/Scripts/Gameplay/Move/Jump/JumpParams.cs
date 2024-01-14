using UnityEngine;
using System;

namespace Game.Scripts.Gameplay.Input
{
    [Serializable]
    public struct JumpParams
    {
        public Rigidbody2D Body;
        public float Force;
    }
}