using LittleMars.Common.Signals;
using Zenject;

namespace LittleMars.TooltipSystem
{
    public class TooltipMenu
    {
        readonly TooltipManager _tooltipManager;
        readonly SignalBus _signalBus;

        public TooltipMenu(TooltipManager tooltipManager, SignalBus signalBus)
        {
            _tooltipManager = tooltipManager;
            _signalBus = signalBus;
        }

        public void Open()
        {
            _signalBus.Subscribe<TooltipStartTouchSignal>(CallForHideTooltip);
        }

        public void CallForHideTooltip()
        {
            _tooltipManager.CallForHideTooltip();
            _signalBus.Unsubscribe<TooltipStartTouchSignal>(CallForHideTooltip);
        }
    }
}
