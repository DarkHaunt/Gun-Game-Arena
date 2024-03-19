using System.Collections.Generic;
using UnityEngine;
using Transform = log4net.Util.Transform;

namespace Game.Scripts.Gameplay.Environment
{
    public class EnvironmentData : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public List<Transform> WeaponSpawnPoints { get; private set; }
        [field: SerializeField] public List<Transform> EnemiesSpawnPoints { get; private set; }
    }
}