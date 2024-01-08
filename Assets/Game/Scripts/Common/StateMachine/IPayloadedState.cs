namespace Game.Scripts.Common.StateMachine
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}