using Game.Scripts.Common.StateMachine;
using Game.Scripts.Menu.UI;
using Photon.Realtime;
using Photon.Pun;
using Zenject;
using static Game.Scripts.Infrastructure.StaticData.InfrastructureKeys;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class CreateRoom : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly CreateRoomView _view;
        
        private int _currentPlayersCount;
        
        
        public CreateRoom(MenuStateMachine stateMachine, CreateRoomView view)
        {
            _stateMachine = stateMachine;
            _view = view;

            _currentPlayersCount = MinPlayersCount;
        }
        
        
        public void Enter()
        {
            _view.Enable(true);
            _view.UpdatePlayerCount(_currentPlayersCount);
            
            _view.CreateButton.onClick.AddListener(CreateAndJoinRoom);
            _view.CancelButton.onClick.AddListener(SetMainMenuState);
            
            _view.IncreaseButton.onClick.AddListener(IncreaseRoomPlayerCount);
            _view.DecreaseButton.onClick.AddListener(DecreaseRoomPlayerCount);
        }

        public void Exit()
        {
            _view.Enable(false);
            
            _view.CreateButton.onClick.RemoveListener(CreateAndJoinRoom);
            _view.CancelButton.onClick.RemoveListener(SetMainMenuState);
            
            _view.IncreaseButton.onClick.AddListener(IncreaseRoomPlayerCount);
            _view.DecreaseButton.onClick.AddListener(DecreaseRoomPlayerCount);
        }

        private void IncreaseRoomPlayerCount()
        {
            if(_currentPlayersCount >= MaxPlayersCount)
                return;

            _currentPlayersCount++;
            _view.UpdatePlayerCount(_currentPlayersCount);
        }     
        
        private void DecreaseRoomPlayerCount()
        {
            if(_currentPlayersCount <= MinPlayersCount)
                return;

            _currentPlayersCount--;
            _view.UpdatePlayerCount(_currentPlayersCount);
        }

        private void CreateAndJoinRoom()
        {
            var roomOption = new RoomOptions
            {
                MaxPlayers = _currentPlayersCount
            };
            
            PhotonNetwork.CreateRoom(_view.GetRoomName(), roomOption);
            
            _stateMachine.Enter<LoadLobby>();
        }
        
        private void SetMainMenuState()
            => _stateMachine.Enter<MainMenu>();
        
        public class Factory : PlaceholderFactory<MenuStateMachine, CreateRoom> {}
    }
}