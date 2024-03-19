using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Configs
{
    [CreateAssetMenu(fileName = "AvailableWeaponsConfig", menuName = "Scriptables/AvailableWeaponsConfig")]
    public class AvailableWeaponsConfig : ScriptableObject
    {
        [field: SerializeField] public List<WeaponView> AvailableWeapons { get; private set; }
    }
}