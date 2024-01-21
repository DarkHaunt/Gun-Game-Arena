using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Grounding
{
    [Serializable]
    public struct Ground
    {
        public LayerMask groundLayer;
        public bool isGrounded;
    }
}