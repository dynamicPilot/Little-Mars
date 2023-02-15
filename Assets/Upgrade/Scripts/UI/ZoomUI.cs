using LittleMars.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class ZoomUI : MonoBehaviour
    {
        [SerializeField] Button _closerButton;
        [SerializeField] Button _farButton;

        ZoomControl _zoomControl;

        [Inject]
        public void Constructor(ZoomControl zoomControl)
        {
            _zoomControl = zoomControl;
        }

        private void OnEnable() => SetListeners();

        private void OnDisable() => RemoveListeners();

        void SetListeners()
        {
            _closerButton.onClick.AddListener(_zoomControl.ZoomIn);
            _farButton.onClick.AddListener(_zoomControl.ZoomOut);
        }

        void RemoveListeners()
        {
            _closerButton.onClick.RemoveAllListeners();
            _farButton.onClick.RemoveAllListeners();
        }
    }
}
