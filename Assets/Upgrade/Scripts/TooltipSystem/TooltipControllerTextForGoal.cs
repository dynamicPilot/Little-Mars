using LittleMars.Localization;
using System;


namespace LittleMars.TooltipSystem
{
    public class TooltipControllerTextForGoal
    {
        readonly LevelLangsManager _levelLangManager;
        readonly InfoLangManager _infoLangManager;

        readonly string _groupPrefix = "goal_";

        public TooltipControllerTextForGoal(LevelLangsManager levelLangManager, InfoLangManager infoLangManager)
        {
            _levelLangManager = levelLangManager;
            _infoLangManager = infoLangManager;
        }

        public string GetText(int index)
        {
            var tag = String.Concat(_groupPrefix, index.ToString());
            var text = _levelLangManager.GetText(tag, Common.TagGroup.level);

            if (text == null || text == "")
            {
                // get common text for goal
                text = _infoLangManager.GetText("goalCommon", Common.TagGroup.info);
            }

            return text;
        }
    }
}
