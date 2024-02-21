using UnityEngine;

namespace Game.Scripts.Gameplay.Entities.Player
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
    }
}