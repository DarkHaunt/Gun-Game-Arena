using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI
{
    public class CreateRoomView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        [field: Space]
        [field: SerializeField] public Button CreateButton { get; private set; }
        [field: SerializeField] public Button CancelButton { get; private set; }
        [SerializeField] private TMP_InputField _roomNameField;
        
        
        [field: Header("--- Count Set Up ---")]
        [field: SerializeField] public Button DecreaseButton { get; private set; }
        [field: SerializeField] public Button IncreaseButton { get; private set; }
        [SerializeField] private TextMeshProUGUI _playerCountField;
        

        public void Enable(bool enable)
            => _canvas.enabled = enable;

        public void UpdatePlayerCount(int count)
            => _playerCountField.text = count.ToString();
        
        public string GetRoomName()
            => _roomNameField.text;
    }
}