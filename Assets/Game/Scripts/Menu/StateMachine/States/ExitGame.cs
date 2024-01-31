using Game.Scripts.Common.StateMachine;
using Zenject;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class ExitGame : IState
    {
        public void Enter()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Exit() { }
        
        public class Factory : PlaceholderFactory<ExitGame> {}
    }
}