using Game.Scripts.Infrastructure.StaticData;
using Photon.Pun;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Connection
{
    public class PhotonConnector
    {
        private readonly PhotonConnectSettings _settings;
        
        public PhotonConnector()
        {
            _settings = Resources.Load<PhotonConnectSettings>(InfrastructureKeys.PhotonSettingsPath);
        }

        public void Connect()
        {
            Debug.Log($"<color=blue>Connecting Photon...</color>");
            
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _settings.AppVersion;            
        }        
    }
}