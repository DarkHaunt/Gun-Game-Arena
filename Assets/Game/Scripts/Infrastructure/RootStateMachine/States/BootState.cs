using Game.Scripts.Infrastructure.Boot;
using Cysharp.Threading.Tasks;
using Game.Scripts.Infrastructure.Connection;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.RootStateMachine.States
{
    public class BootState : IAsyncState
    {
        private readonly PhotonConnector _connector;

        public BootState(PhotonConnector connector)
        {
            _connector = connector;
        }
        
        public async UniTask Enter()
        {
            Debug.Log($"<color=red>--- BOOT ---</color>");
            
            _connector.Connect();
            
            await UniTask.Yield();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }
        
        public class Factory : PlaceholderFactory<BootState> {}
    }
}
        