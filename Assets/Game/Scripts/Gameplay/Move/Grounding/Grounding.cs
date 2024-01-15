using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Move.Grounding
{
    [Serializable]
    public struct Grounding
    {
        public LayerMask GroundLayer;
        public Collider2D GroundChecker;
    }
}