using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.Assets
{
    public class AssetProvider
    {
        private static readonly Dictionary<Type, string> _prewarmAssets = new()
        {
    
        };

        private readonly Dictionary<Type, Object> _assets = new();


        public async void PrewarmAssets()
        {
            foreach (var VARIABLE in _prewarmAssets)
            {
                //var t = Resources.LoadAsync<>()
            }
        }
    }
}