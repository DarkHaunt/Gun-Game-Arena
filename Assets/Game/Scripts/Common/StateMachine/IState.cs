namespace Game.Scripts.Common.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}