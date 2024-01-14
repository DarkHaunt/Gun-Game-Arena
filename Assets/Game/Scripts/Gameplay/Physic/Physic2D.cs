using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Physic
{
    [Serializable]
    public struct Physic2D
    {
        public Rigidbody2D Body;
        public Collider2D Collider;
    }
}