using Game.Scripts.Infrastructure.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.RootStateMachine.States
{
    public class GameplayState : IAsyncState
    {
        private readonly AssetProvider _assetProvider;

        public GameplayState(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Enter()
        {
            Debug.Log($"<color=red>--- GAME ---</color>");

            await PrewarmAssets();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }

        private async UniTask PrewarmAssets()
        {
            var tasks = new UniTask[]
            {
            };

            await UniTask.WhenAll(tasks);
        }

        public class Factory : PlaceholderFactory<GameplayState> {}
    }
}