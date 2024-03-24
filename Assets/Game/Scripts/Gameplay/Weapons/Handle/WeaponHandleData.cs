using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    [Serializable]
    public struct WeaponHandleData
    {
        [HideInInspector] public float HandledDuration;
        public float Duration;
    }
}