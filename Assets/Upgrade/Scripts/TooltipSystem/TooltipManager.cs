using LittleMars.Signals;
using LittleMars.UI.Tooltip;
using UnityEngine;
using Zenject;
using LittleMars.UI.Windows;

namespace LittleMars.TooltipSystem
{
    /// <summary>
    /// Level Tooltip Manager.
    /// </summary>
    public class TooltipManager : IInitializable
    {
        readonly TooltipFactory _factory;
        readonly SceneWindows _sceneWindows;
        readonly SignalBus _signalBus;

        TooltipObject _tooltip;

        public TooltipManager(TooltipFactory factory, SceneWindows sceneWindows, SignalBus signalBus)
        {
            _factory = factory;
            _sceneWindows = sceneWindows;
            _signalBus = signalBus;
            _tooltip = null;
        }

        public void Initialize()
        {

        }

        public void CallForTooltip(TooltipContext context)
        {
            Debug.Log("TooltipManager: called for tooltip");
            if (context == null) return;
            else if (_tooltip == null) CreateTooltip();
            else if (_tooltip.gameObject.activeSelf) HideTooltip();

            ShowTooltip(context);
        }

        public void CallForHideTooltip()
        {
            Debug.Log("TooltipManager: called for hide tooltip");
            if (_tooltip.gameObject.activeSelf)
                HideTooltip();

            _signalBus.Fire<CallForHideTooltipSignal>();
        }

        void ShowTooltip(TooltipContext context)
        {
            SetTooltipToTopOrder();
            _tooltip.Open(context);
            _signalBus.Fire<CallForTooltipSignal>();
        }

        void HideTooltip()
        {
            _tooltip.Close();
        }

        void SetTooltipToTopOrder()
        {
            _tooltip.gameObject.transform.SetSiblingIndex(_sceneWindows.Canvas.GetSiblingIndex() - 1);
        }

        void CreateTooltip()
        {
            Debug.Log("Called for creating tooltip....");
            _tooltip = _factory.Create(_sceneWindows.Canvas);
        }
    }
}
