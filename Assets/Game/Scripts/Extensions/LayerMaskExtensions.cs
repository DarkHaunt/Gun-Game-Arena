using UnityEngine;
using System;

namespace Game.Scripts.Extensions
{
    public static class LayerMaskExtensions
    {
        public static LayerMask GetMaskFromLayer(int layer)
            => (int)Math.Pow(2, layer);

        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            var bit = 1 << layer;

            return (mask.value & bit) != 0;
        }
        
        public static int ToLayer(int bitmask)
        {
            int result = bitmask > 0 ? 0 : 31;
            while (bitmask > 1)
            {
                bitmask >>= 1;
                result++;
            }
            return result;
        }
        
        public static int ToLayer(LayerMask layerMask)
        {
            int bitmask = layerMask.value;
            int result = bitmask > 0 ? 0 : 31;
            while (bitmask > 1)
            {
                bitmask >>= 1;
                result++;
            }
            return result;
        }
    }
}