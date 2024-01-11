using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.RootStateMachine.States
{
    public class LobbyState : IAsyncState
    {
        public async UniTask Enter()
        {
            Debug.Log($"<color=red>--- LOBBY ---</color>");

            await UniTask.Yield();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }

        public class Factory : PlaceholderFactory<LobbyState> {}
    }
}