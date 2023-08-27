using DG.Tweening.Core;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// Factory class to create TooltipControllerObject and text ui for building slot.
    /// </summary>
    public class BuildingSlotTooltipControllerFactory
    {
        readonly BuildingSlotTooltipControllerTextUI.Factory _factory;

        public BuildingSlotTooltipControllerFactory(BuildingSlotTooltipControllerTextUI.Factory factory)
        {
            _factory = factory;
        }

        public BuildingSlotTooltipControllerTextUI CreateSlot(string[] tags, TooltipType type, RectTransform container)
        {
            var slot = _factory.Create(tags, type);
            slot.transform.SetParent(container);
            //slot.transform.SetSiblingIndex();

            slot.transform.localScale = new Vector3(1f, 1f, 1f);
            slot.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
            slot.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
            //_setter.SetSlot(slot, type);
            return slot;
        }
    }
}
