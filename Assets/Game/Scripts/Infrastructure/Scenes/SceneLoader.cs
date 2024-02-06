using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Infrastructure.Scenes
{
    public class SceneLoader
    {
        private readonly SceneTransitionHandler _transitionHandler;

        public SceneLoader(SceneTransitionHandler transitionHandler)
        {
            _transitionHandler = transitionHandler;
        }
        
        public async UniTask LoadScene(Scene scene)
        {
            var name = scene.ToString();

            await _transitionHandler.PerformTransition();

            var loadingScreenDelay = SceneTransitionHandler.PerformForceLoadScreen();
            var sceneLoading = LoadSceneAsync(name);
            
            await UniTask.WhenAll(loadingScreenDelay, sceneLoading);
            
            await _transitionHandler.PerformUnfade();
        }

        private UniTask LoadSceneAsync(string name)
            => SceneManager.LoadSceneAsync(name).ToUniTask();
    }
}