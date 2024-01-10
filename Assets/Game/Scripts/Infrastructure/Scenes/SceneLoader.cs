using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Infrastructure.Scenes
{
    public class SceneLoader
    {
        public async UniTask LoadScene(Scene scene)
        {
            var name = scene.ToString();

            await SceneManager.LoadSceneAsync(name).ToUniTask();
        }
        
        
    }
}