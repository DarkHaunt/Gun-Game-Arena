using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptables/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveForce { get; private set; }
    }
}