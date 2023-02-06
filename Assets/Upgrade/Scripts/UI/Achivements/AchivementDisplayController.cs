using LittleMars.Common;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;

namespace LittleMars.UI.Achivements
{
    public class AchivementDisplayController
    {
        readonly GoalDisplayStrategiesManager _manager;
        readonly TimeSpeedManager _timeSpeedManager;
        public AchivementDisplayController(GoalDisplayStrategiesManager manager, TimeSpeedManager timeSpeedManager)
        {
            _manager = manager;
            _timeSpeedManager = timeSpeedManager;
        }

        public void Open()
        {
            // change timespeed
            _timeSpeedManager.Stop();
        }

        public void Close()
        {
            // change timespeed
            _timeSpeedManager.Start();
        }

        public IGoalDisplayStrategy GetDisplayStrategy(int index)
        {
            return _manager.GetStrategy(index);
        }  
    }
}
