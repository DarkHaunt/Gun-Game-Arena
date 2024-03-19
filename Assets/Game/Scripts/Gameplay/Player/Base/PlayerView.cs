using Game.Scripts.Gameplay.Entities;
using Game.Scripts.Gameplay.Weapons;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player.Base
{
    public class PlayerView : EcsEntityView
    {
        [field: SerializeField] public WeaponPicker WeaponPicker { get; private set; }

        [field: Header("--- Common ---")]
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
    }
}