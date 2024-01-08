#if UNITY_EDITOR
using UnityEngine;

namespace Game.Scripts.Common.Validation
{
    [ExecuteInEditMode]
    public class UICameraCanvasValidator : MonoBehaviour
    {
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            var obj = GameObject.Find(ValidateKeys.UICameraName);

            if (obj && obj.TryGetComponent(out Camera uiCamera))
            {
                canvas.worldCamera = uiCamera;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
            }
        }
    }
}
#endif
