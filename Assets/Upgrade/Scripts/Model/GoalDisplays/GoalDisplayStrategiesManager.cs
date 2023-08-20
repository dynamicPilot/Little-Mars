using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.GoalDisplays
{
    /// <summary>
    /// Call factory to create IGoalDisplayStrategies and keep all strategies.
    /// </summary>
    public class GoalDisplayStrategiesManager : IInitializable
    {
        readonly GoalDisplayStrategiesFactory _factory;
        readonly SignalBus _signalBus;

        List<IGoalDisplayStrategy> _strategies;

        public GoalDisplayStrategiesManager(GoalDisplayStrategiesFactory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            CreateStrategies();
        }

        private void CreateStrategies()
        {
            _strategies = _factory.CreateStrategies();
            //Debug.Log("Strategies is created : " + _strategies.Count);
            _signalBus.Fire(new GoalStrategiesIsReadySignal { Strategies = _strategies.ToArray() });
        }

        public IGoalDisplayStrategy[] GetStrategies()
        {
            return _strategies.ToArray();
        }

        public IGoalDisplayStrategy GetStrategy(int index)
        {
            //Debug.Log("Need strategy with index : " + index);
            if (index >= _strategies.Count || index < 0) return null;
            return _strategies[index];
        }

        //public IGoalDisplayStrategy[] GetStates()
        //{
        //    return _strategies.ToArray();
        //}

        public void DisposeStrategy(int index)
        {
            _strategies[index].Dispose();
            _strategies[index] = null;

        }
    }
}

