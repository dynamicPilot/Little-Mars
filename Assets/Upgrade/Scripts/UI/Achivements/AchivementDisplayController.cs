using LittleMars.Common;
using LittleMars.Model.GoalDisplays;
using LittleMars.UI.GoalDisplays;

namespace LittleMars.UI.Achivements
{
    public class AchivementDisplayController
    {
        readonly GoalDisplayStatesManager _manager;

        public AchivementDisplayController(GoalDisplayStatesManager manager)
        {
            _manager = manager;
        }

        public void Open()
        {
            // change timespeed
        }

        public void Close()
        {
            // change timespeed
        }

        public IGoalDisplayStrategy GetDisplayStrategy(int index)
        {
            return _manager.GetStrategy(index);
        }  
    }
}
