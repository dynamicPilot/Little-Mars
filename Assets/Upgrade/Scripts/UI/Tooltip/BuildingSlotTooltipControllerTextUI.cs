using LittleMars.Common;
using LittleMars.TooltipSystem;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// TooltipController text ui for building slot. Must be created using PlaceholderFactory subclass.
    /// </summary>
    public class BuildingSlotTooltipControllerTextUI : TooltipControllerTextUI
    {
        string[] _tags; // tags for info text blocks
        TooltipType _type; // type (field, connections) for common text
        TooltipControllerTextForBuildSlots _textForBuildingSlot;

        [Inject]
        public void Constructor(TooltipControllerTextForBuildSlots textForBuildingSlot, string[] tags, TooltipType type)
        {
            _textForBuildingSlot = textForBuildingSlot;
            _tags = tags;
            _type = type;
        }

        protected override void SetText()
        {
            _text = "test";
            //_text = _textForBuildingSlot.GetText(_tags, _type);
        }

        public class Factory : PlaceholderFactory<string[], TooltipType, BuildingSlotTooltipControllerTextUI>
        { }
    }
}
