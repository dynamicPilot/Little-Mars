using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.UI.GoalDisplays;
using System.Collections.Generic;

namespace LittleMars.Model.GoalDisplays
{
    public class DisplayStrategiesFactory
    {
        readonly DisplayStrategyFactory _strategyFactory;

        public DisplayStrategiesFactory(DisplayStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public List<IGoalDisplayStrategy> CreateSlots<T>(GoalType type, Goals<T> goals, 
            GoalInfoFactory<T> factory)
        {
            var slots = new List<IGoalDisplayStrategy>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                var info = factory.Create(type, goals.GoalsArray[i].Unit);
                slots.Add(_strategyFactory.CreateStrategy(info));
            }

            return slots;
        }


        public List<IGoalDisplayStrategy> CreateSlotsWithTimer<T>(GoalType type, 
            GoalsWithTimer<T> goals, WithTimerGoalInfoFactory<T> factory)
        {
            var slots = new List<IGoalDisplayStrategy>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
                slots.Add(CreateSlotWithTimer<T>(type, goals.GoalsArray[i], factory));

            return slots;
        }

        public IGoalDisplayStrategy CreateSlotWithTimer<T>(GoalType type, GoalWithTime<T> goal,
            WithTimerGoalInfoFactory<T> factory)
        {
            var info = factory.Create(type, goal.Time, goal.Unit);
            return _strategyFactory.CreateStrategy(info);
        }
    }
}
