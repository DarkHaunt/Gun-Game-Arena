using UnityEngine;

namespace Game.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "PhotonSettings", menuName = "Scriptables/PhotonSettings")]
    public class PhotonConnectSettings : ScriptableObject
    {
        [field: SerializeField] public string AppVersion { get; private set; }
    }
}