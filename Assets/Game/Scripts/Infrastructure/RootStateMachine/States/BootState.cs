using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.RootStateMachine.States
{
    public class BootState : IAsyncState
    {
        public async UniTask Enter()
        {
            Debug.Log($"<color=red>--- BOOT ---</color>");
            
            await UniTask.Yield();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }
        
        public class Factory : PlaceholderFactory<BootState> {}
    }
}
        