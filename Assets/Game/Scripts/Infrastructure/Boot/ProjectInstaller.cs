using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Boot
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log($"<color=#76d1e3>ProjectInstaller Executed</color>");
        }
    }
}