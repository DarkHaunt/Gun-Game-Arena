using UnityEngine;

namespace Game.Scripts.Gameplay.Enemy.Follow
{
    public struct EnemyTargetFollower
    {
        public float StopFollowDistance;
        public Transform Target;
        public Transform Self;

        public Vector3 TargetPos
            => Target.position;
        
        public Vector3 SelfPos
            => Self.position;
    }
}