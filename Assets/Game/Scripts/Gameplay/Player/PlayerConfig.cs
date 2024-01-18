using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptables/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float MoveForce { get; private set; }

        [field: Space]
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}