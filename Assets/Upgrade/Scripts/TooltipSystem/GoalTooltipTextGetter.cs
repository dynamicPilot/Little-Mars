using LittleMars.Common;
using LittleMars.Localization;
using System;


namespace LittleMars.TooltipSystem
{
    public class GoalTooltipTextGetter
    {
        readonly LevelLangsManager _levelLangManager;
        readonly InfoLangManager _infoLangManager;

        readonly string _groupPrefix = "goal_";

        public GoalTooltipTextGetter(LevelLangsManager levelLangManager, InfoLangManager infoLangManager)
        {
            _levelLangManager = levelLangManager;
            _infoLangManager = infoLangManager;
        }

        public string GetText(int index, bool _withTimer)
        {
            var tag = String.Concat(_groupPrefix, index.ToString());
            var text = _levelLangManager.GetText(tag, Common.TagGroup.level);

            if (_withTimer)
            {
                index++;
                tag = String.Concat(_groupPrefix, index.ToString());
                text = string.Concat(text, " ", _levelLangManager.GetText(tag, Common.TagGroup.level));
            }

            if (text == null || text == "")
            {
                // get common text for goal
                text = _infoLangManager.GetText("goalCommon", Common.TagGroup.info);
            }

            return text;
        }
    }
}
