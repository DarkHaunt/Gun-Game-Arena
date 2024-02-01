using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI
{
    public class JoinRoomView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        [field: Space]
        [field: SerializeField] public Button JoinButton { get; private set; }
        [field: SerializeField] public Button CancelButton { get; private set; }
        [SerializeField] private TMP_InputField _roomNameField;
        
        
        public void Enable(bool enable)
            => _canvas.enabled = enable;
        
        public string GetRoomName()
            => _roomNameField.text;
    }
}