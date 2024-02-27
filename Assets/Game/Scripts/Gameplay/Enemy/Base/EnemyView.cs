using UnityEngine;

namespace Game.Scripts.Gameplay.Entities.Enemy.Base
{
    public class EnemyView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
    }
}