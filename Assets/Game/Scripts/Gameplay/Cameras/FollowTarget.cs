using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Cameras
{
    [Serializable]
    public struct FollowTarget
    {
        public Transform Target;
        public Transform Self;
        public Vector3 Offset;

        public Vector3 TargetPos
            => Target.position;
    }
}