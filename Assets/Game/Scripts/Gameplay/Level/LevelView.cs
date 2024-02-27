using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay.Level
{
    public class LevelView : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public List<Transform> EnemiesSpawnPoints { get; private set; }
    }
}