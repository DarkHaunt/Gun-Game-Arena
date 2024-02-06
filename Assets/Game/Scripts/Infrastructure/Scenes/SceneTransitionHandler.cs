using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using System;

namespace Game.Scripts.Infrastructure.Scenes
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        private const float FadeTime = 1f;
        private const float UnfadeTime = 0.5f;
        private const float ForceLoadingScreenTime = 1f;
        
        [SerializeField] private Image _fade;
        [SerializeField] private Canvas _canvas;

        
        public async UniTask PerformTransition()
        {
            await _fade.DOFade(1f, FadeTime).AsyncWaitForCompletion();

            ActivateLoadingScreen(true);
        }

        public async Task PerformUnfade()
        {
            ActivateLoadingScreen(false);
            
            await _fade.DOFade(0f, UnfadeTime).AsyncWaitForCompletion();
        }

        public static async UniTask PerformForceLoadScreen()
            => await UniTask.Delay(TimeSpan.FromSeconds(ForceLoadingScreenTime));

        private void ActivateLoadingScreen(bool enable)
            => _canvas.enabled = enable;
    }
}