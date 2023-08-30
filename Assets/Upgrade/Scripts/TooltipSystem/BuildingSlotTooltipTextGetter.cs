using LittleMars.Common;
using LittleMars.Localization;


namespace LittleMars.TooltipSystem
{
    /// <summary>
    /// Class to get text for the building slot tooltip (connections or field type).
    /// </summary>
    public class BuildingSlotTooltipTextGetter
    {
        readonly InfoLangManager _infoLangManager;

        public BuildingSlotTooltipTextGetter(InfoLangManager infoLangManager)
        {
            _infoLangManager = infoLangManager;
        }

        public string GetText(string[] tags, TooltipType type)
        {
            var text = GetCommonPart(type);

            for (int i = 0; i < tags.Length; i++)
            {
                var separator = (i == tags.Length - 1) ? "" : ",";
                text = string.Concat(text, " ", _infoLangManager.GetText(tags[i], Common.TagGroup.info), separator); 
            }

            text = string.Concat(text, ".");
            return text;
        }

        string GetCommonPart(TooltipType type)
        {
            var commonText = string.Concat(type.ToString(), "Common");
            return _infoLangManager.GetText(commonText, Common.TagGroup.info);
        }
    }
}
