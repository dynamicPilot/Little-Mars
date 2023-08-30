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
        [SerializeField] TooltipInfo _tooltipInfo;

        [Header ("Tooltip Settings")]
        [SerializeField] Direction _direction;
        [SerializeField] float _offset;

        TooltipController _controller;
        //SignalBus _signalBus;

        //int _clickCount = 0;

        [Inject]
        public void Constructor(TooltipController controller)//, SignalBus signalBus)
        {
            _controller = controller;
            //_signalBus = signalBus;
            //_clickCount = 0;
            Init();
        }

        void Init()
        {
            _button.onClick.AddListener(OnButtonClick);
            //_signalBus.Subscribe<CallForHideTooltipSignal>(ResetClickCount);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        void OnButtonClick()
        {
            Debug.Log("TooltipControllerUI: call for tooltip");
            _controller.CallTooltip(GetTooltipContext(), _rectTransform);

            //_clickCount++;
            //Debug.Log("TooltipControllerUI: counter is " + _clickCount);
            
            //if (_clickCount == 1)
            //{
                
            //}
                
        }

        //void ResetClickCount()
        //{
        //    Debug.Log("TooltipControllerUI: reset counter");
        //    // reset click count if player
        //    // clicks again after tooltip was shown
        //    _clickCount = 0;
        //}

        TooltipContext GetTooltipContext()
        {
            return new TooltipContext(_direction, _offset, _rectTransform, _tooltipInfo.GetText());
        }
    }
}
