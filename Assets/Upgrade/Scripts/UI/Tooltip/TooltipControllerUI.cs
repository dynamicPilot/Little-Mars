using LittleMars.Common;
using LittleMars.Signals;
using LittleMars.TooltipSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    public class TooltipControllerUI: MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] Button _button;

        [Header ("Tooltip Settings")]
        [SerializeField] Direction _direction;
        [SerializeField] float _offset;

        TooltipController _controller;
        SignalBus _signalBus;

        int _clickCount = 0;

        [Inject]
        public void Constructor(TooltipController controller, SignalBus signalBus)
        {
            _controller = controller;
            _signalBus = signalBus;
            _clickCount = 0;
            Init();
        }

        void Init()
        {
            _button.onClick.AddListener(OnButtonClick);
            _signalBus.Subscribe<CallForHideTooltipSignal>(ResetClickCount);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        void OnButtonClick()
        {
            //Debug.Log("Button click " + _clickCount);
            _clickCount++;

            if (_clickCount == 1)
                _controller.CallTooltip(GetTooltipContext());
        }

        void ResetClickCount()
        {
            // reset click count if player
            // clicks again after tooltip was shown
            _clickCount = 0;
        }

        TooltipContext GetTooltipContext()
        {
            return new TooltipContext(_direction, _offset, _rectTransform);
        }
    }
}
