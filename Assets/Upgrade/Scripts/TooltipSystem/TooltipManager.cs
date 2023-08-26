using LittleMars.Signals;
using LittleMars.UI.Tooltip;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.TooltipSystem
{
    public class TooltipManager : IInitializable
    {
        readonly TooltipObject.Factory _factory;

        TooltipObject _tooltip;

        public TooltipManager(TooltipObject.Factory factory)
        {
            _factory = factory;
            _tooltip = null;
        }

        public void Initialize()
        {

        }

        public void CallForTooltip(TooltipContext context)
        {
            Debug.Log("Called for tooltip");
            if (context == null) return;
            else if (_tooltip == null) CreateTooltip();
            else if (_tooltip.gameObject.activeSelf) HideTooltip();

            ShowTooltip(context);
        }

        public void CallForHideTooltip()
        {
            if (_tooltip.gameObject.activeSelf)
                HideTooltip();
        }

        void ShowTooltip(TooltipContext context)
        {
            _tooltip.Open(context);
        }

        void HideTooltip()
        {
            _tooltip.Close();
        }

        void CreateTooltip()
        {
            Debug.Log("Called for creating tooltip....");
            _tooltip = _factory.Create();
            Debug.Log("Create tooltip");
        }


    }
}
