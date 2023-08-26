using LittleMars.Commands;
using LittleMars.Commands.Level;
using LittleMars.LevelMenus;
using Zenject;

namespace LittleMars.TooltipSystem
{
    public class TooltipMenu
    {
        readonly TooltipManager _tooltipManager;

        public TooltipMenu(TooltipManager tooltipManager)
        {
            _tooltipManager = tooltipManager;
        }

        public void CallForHideTooltip()
        {
            _tooltipManager.CallForHideTooltip();
        }
    }
}
