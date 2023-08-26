using LittleMars.Common.Signals;
using LittleMars.Signals;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.TooltipSystem
{
    public class TooltipController
    {
        readonly TooltipManager _manager;

        public TooltipController(TooltipManager manager)
        {
            _manager = manager;
        }

        public void CallTooltip(TooltipContext context)
        {
            Debug.Log("Call for tooltip by TooltipManager");
            _manager.CallForTooltip(context);
        }
    }
}
