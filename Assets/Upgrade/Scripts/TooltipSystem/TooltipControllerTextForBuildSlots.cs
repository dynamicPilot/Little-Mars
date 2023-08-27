using LittleMars.Common;
using LittleMars.Localization;


namespace LittleMars.TooltipSystem
{
    /// <summary>
    /// Class to get text for the building slot tooltip (connections or field type).
    /// </summary>
    public class TooltipControllerTextForBuildSlots
    {
        readonly InfoLangManager _infoLangManager;
        public string GetText(string[] tags, TooltipType type)
        {
            //var tag = String.Concat(_groupPrefix, index.ToString());
            var text = "";//_infoLangManager.GetText("goalCommon", Common.TagGroup.info);

            return text;
        }
    }
}
