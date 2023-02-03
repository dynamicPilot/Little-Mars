using LittleMars.Common;
using System;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingStateManager
    {
        readonly BuildingOnStateFactory _onStateFactory;
        readonly BuildingOffStateFactory _offStateFactory;
        readonly BuildingPausedStateFactory _pausedStateFactory;

        IBuildingState _onState = null;
        IBuildingState _offState = null;
        IBuildingState _pausedState = null;

        public BuildingStateManager(BuildingOnStateFactory onStateFactory,
            BuildingOffStateFactory offStateFactory, BuildingPausedStateFactory pausedStateFactory)
        {
            _onStateFactory = onStateFactory;
            _offStateFactory = offStateFactory;
            _pausedStateFactory = pausedStateFactory;
        }

        public IBuildingState CreateState(BStates state)
        {
            if (state == BStates.on)
            {
                if (_onState == null) _onState = _onStateFactory.Create();
                return _onState;
            }
            else if (state == BStates.off)
            {
                if (_offState == null) _offState = _offStateFactory.Create();
                return _offState;
            }
            else if (state == BStates.paused)
            {
                if (_pausedState == null) _pausedState = _pausedStateFactory.Create();
                return _pausedState;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }


}
