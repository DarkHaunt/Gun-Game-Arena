using UnityEngine;

namespace Game.Scripts.Gameplay.Entities.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptables/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveForce { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
    }
}