using UnityEngine;

namespace Game.Scripts.Gameplay.Cameras
{
    public struct CameraFollow
    {
        public Transform Target;
        public Transform Self;
        public Vector3 Offset;

        public Vector3 TargetPos
            => Target.position;
    }
}