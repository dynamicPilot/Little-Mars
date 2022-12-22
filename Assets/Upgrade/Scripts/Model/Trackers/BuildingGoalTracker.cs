using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class BuildingGoalTracker : IGoalTracker
    {
        readonly Goal<BuildingUnit<int>> _goal;
        readonly SignalBus _signalBus;

        List<IBuildingFacade> _buildings;
        bool _isDone = false;
        public BuildingGoalTracker(Goal<BuildingUnit<int>> goal, SignalBus signalBus)
        {
            _goal = goal;
            _signalBus = signalBus;

            _buildings = new();
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        public bool Check()
        {
            return _isDone;
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal args)
        {
            var info = args.BuildingFacade.Info();

            if (_goal.Unit.Type != info.Type || _goal.Unit.Size != info.Size) return;

            if (args.BuildingFacade.State() == ProductionState.on && !_buildings.Contains(args.BuildingFacade))
                _buildings.Add(args.BuildingFacade);
            else if (args.BuildingFacade.State() == ProductionState.off && _buildings.Contains(args.BuildingFacade))
                _buildings.Remove(args.BuildingFacade);
            else return;

            CheckIsDone();
        }

        private void CheckIsDone()
        {
            _isDone = (_buildings.Count >= _goal.Unit.Amount);

            if (_isDone)
            {
                // rise event => goalIsDone
                Debug.Log("Done!");
            }
        }
    }
}
