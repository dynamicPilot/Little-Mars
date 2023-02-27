using LittleMars.AudioSystems;
using LittleMars.Common;
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
        UISoundSystem _audioSystem;
        [Inject]
        public void Constructor(ZoomControl zoomControl, UISoundSystem audioSystem)
        {
            _zoomControl = zoomControl;
            _audioSystem = audioSystem;
        }

        private void OnEnable() => SetListeners();

        private void OnDisable() => RemoveListeners();

        void SetListeners()
        {
            _closerButton.onClick.AddListener(ZoomIn);
            _farButton.onClick.AddListener(ZoomOut);
        }

        void ZoomIn()
        {
            _audioSystem.PlayUISound(UISoundType.zoomIn);
            _zoomControl.ZoomIn();
        }

        void ZoomOut()
        {
            _audioSystem.PlayUISound(UISoundType.zoomOut);
            _zoomControl.ZoomOut();
        }

        void RemoveListeners()
        {
            _closerButton.onClick.RemoveAllListeners();
            _farButton.onClick.RemoveAllListeners();
        }
    }
}
