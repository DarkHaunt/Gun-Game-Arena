using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.Assets
{
    public class AssetProvider
    {
        private readonly Dictionary<string, Object> _cachedObjects = new();


        public async UniTask<T> Get<T>(string path) where T : Object
        {
            if (_cachedObjects.TryGetValue(path, out var prefab))
                return prefab as T;

            await LoadAndCacheAsset<T>(path);
            
            return (T)_cachedObjects[path];
        }

        public async UniTask LoadAndCacheAsset<T>(string path) where T : Object
        {
            var loadRequest = Resources.LoadAsync<T>(path);
            await loadRequest.ToUniTask();

            _cachedObjects[path] = loadRequest.asset;
        }
    }
}