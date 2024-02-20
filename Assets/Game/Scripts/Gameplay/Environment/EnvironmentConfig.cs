using UnityEngine;

namespace Game.Scripts.Gameplay.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentConfig", menuName = "Scriptables/EnvironmentConfig")]
    public class EnvironmentConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 CameraOffset { get; private set; }
    }
}