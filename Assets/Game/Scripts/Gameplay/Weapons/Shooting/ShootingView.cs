using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Shooting
{
    public class ShootingView : MonoBehaviour
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
    }
}