using LittleMars.Common;
using System;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingStateManager
    {
        readonly BuildingOnState.Factory _onStateFactory;
        readonly BuildingOffState.Factory _offStateFactory;
        readonly BuildingPausedState.Factory _pausedStateFactory;

        IBuildingState _onState = null;
        IBuildingState _offState = null;
        IBuildingState _pausedState = null;

        public BuildingStateManager(BuildingOnState.Factory onStateFactory, 
            BuildingOffState.Factory offStateFactory, BuildingPausedState.Factory pausedStateFactory)
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
