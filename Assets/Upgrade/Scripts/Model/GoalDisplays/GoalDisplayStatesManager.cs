using LittleMars.UI.GoalDisplays;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.GoalDisplays
{
    public class GoalDisplayStatesManager : IInitializable
    {
        readonly GoalDisplayStrategiesFactory _factory;
        List<IGoalDisplayStrategy> _strategies;

        public GoalDisplayStatesManager(GoalDisplayStrategiesFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            CreateStrategies();
        }

        private void CreateStrategies()
        {
            _strategies = _factory.CreateStrategies();
            Debug.Log("Strategies is created : " + _strategies.Count);
        }

        public IGoalDisplayStrategy GetStrategy(int index)
        {
            Debug.Log("Need strategy with index : " + index);
            if (index >= _strategies.Count || index < 0) return null;
            return _strategies[index];
        }

        public IGoalDisplayStrategy[] GetStates()
        {
            return _strategies.ToArray();
        }

        public void DisposeStrategy(int index)
        {
            _strategies[index].Dispose();
            _strategies[index] = null;

        }
    }
}

