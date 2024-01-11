using Cysharp.Threading.Tasks;

namespace Game.Scripts.Infrastructure.RootStateMachine
{
    public interface IAsyncState
    {
        UniTask Enter();
        UniTask Exit();
    }
}