using LittleMars.Common;
using LittleMars.Model.GoalDisplays;

namespace LittleMars.UI.GoalDisplays
{
    public class DisplayStrategyFactory
    {
        readonly ResourceGoalDisplayStrategy.Factory _resourceFactory;
        readonly BuildingGoalDisplayStrategy.Factory _buildingFactory;
        readonly BuildingGoalWithTimerDisplayStrategy.Factory _buildingWithTimerFactory;
        readonly BuildingTimerStaffGoalDisplayStrategy.Factory _timerFactory;

        public DisplayStrategyFactory(ResourceGoalDisplayStrategy.Factory resourceFactory, 
            BuildingGoalDisplayStrategy.Factory buildingFactory, 
            BuildingGoalWithTimerDisplayStrategy.Factory buildingWithTimerFactory,
            BuildingTimerStaffGoalDisplayStrategy.Factory timerFactory)
        {
            _resourceFactory = resourceFactory;
            _buildingFactory = buildingFactory;
            _buildingWithTimerFactory = buildingWithTimerFactory;
            _timerFactory = timerFactory;
        }

        public IGoalDisplayStrategy CreateStrategy(IGoalInfo info)
        {
            GoalType type = info.GetType();
            bool withTimer = info.WithTimer();

            if (type == GoalType.time && withTimer)
            {
                return _timerFactory.Create(info);
            }
            
            if (withTimer)
            {
                return _buildingWithTimerFactory.Create(info);
            }

            if (type == GoalType.resources || type == GoalType.production)
            {
                return _resourceFactory.Create(info);
            }
            else if (type == GoalType.building)
            {
                return _buildingFactory.Create(info);
            }

            return null;
        }
    }
}
