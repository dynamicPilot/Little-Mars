using LittleMars.Common.Signals;
using LittleMars.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.TooltipSystem
{
    public class TooltipController : IInitializable, IDisposable
    {
        readonly TooltipManager _manager;
        readonly SignalBus _signalBus;

        RectTransform _lastPressedRectTransform;
        bool _needCheckNextClick = false;

        public TooltipController(TooltipManager manager, SignalBus signalBus)
        {
            _manager = manager;
            _signalBus = signalBus;

            _lastPressedRectTransform = null;
            _needCheckNextClick = false;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<TooltipStartTouchSignal>(OnTooltipStartTouchSignal);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<TooltipStartTouchSignal>(OnTooltipStartTouchSignal);
        }

        public void CallTooltip(TooltipContext context, RectTransform rectTransform)
        {
            //Debug.Log("Call for tooltip by TooltipManager");
            if (_needCheckNextClick)
            {
                var sameRect = CheckForSameLastRectTransform(rectTransform);

                if (sameRect)
                {
                    _lastPressedRectTransform = null;
                    _needCheckNextClick = false;
                    return;
                }
            }

            _needCheckNextClick = false;
            _lastPressedRectTransform = rectTransform;
            _manager.CallForTooltip(context);
        }

        public void HideTooltip()
        {
            _manager.CallForHideTooltip();
        }

        void OnTooltipStartTouchSignal()
        {
            _needCheckNextClick = true;
            HideTooltip();
        }

        bool CheckForSameLastRectTransform(RectTransform rectTransform)
        {
            if (_lastPressedRectTransform == null) return true;
            else return rectTransform == _lastPressedRectTransform;
        }
    }
}
