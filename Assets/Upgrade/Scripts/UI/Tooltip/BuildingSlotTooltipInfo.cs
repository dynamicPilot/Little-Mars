using LittleMars.Common;
using LittleMars.TooltipSystem;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// TooltipController info for building slot. Must be created using PlaceholderFactory subclass.
    /// </summary>
    public class BuildingSlotTooltipInfo : TooltipInfo
    {
        string[] _tags; // tags for info text blocks
        TooltipType _type; // type (field, connections) for common text
        BuildingSlotTooltipTextGetter _textForBuildingSlot;

        [Inject]
        public void Constructor(BuildingSlotTooltipTextGetter textForBuildingSlot, string[] tags, TooltipType type)
        {
            _textForBuildingSlot = textForBuildingSlot;
            _tags = tags;
            _type = type;
        }

        protected override void SetText()
        {
            _text = _textForBuildingSlot.GetText(_tags, _type);
        }

        public class Factory : PlaceholderFactory<string[], TooltipType, BuildingSlotTooltipInfo>
        { }
    }
}