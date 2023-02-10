using LittleMars.Common;
using System;

namespace LittleMars.Buildings.View.States
{
    public class BuildingViewStatesManager
    {
        readonly BuildingViewOnState.Factory _onStateFactory;
        readonly BuildingViewOffState.Factory _offStateFactory;
        readonly BuildingViewPausedState.Factory _pausedStateFactory;

        IViewState _onState = null;
        IViewState _offState = null;
        IViewState _pausedState = null;

        public BuildingViewStatesManager(BuildingViewOnState.Factory onStateFactory, 
            BuildingViewOffState.Factory offStateFactory, BuildingViewPausedState.Factory pausedStateFactory)
        {
            _onStateFactory = onStateFactory;
            _offStateFactory = offStateFactory;
            _pausedStateFactory = pausedStateFactory;
        }

        public IViewState CreateState(BStates state)
        {
            if (state == BStates.on)
            {
                _onState ??= _onStateFactory.Create();
                return _onState;
            }
            else if (state == BStates.off)
            {
                _offState ??= _offStateFactory.Create();
                return _offState;
            }
            else if (state == BStates.paused)
            {
                _pausedState ??= _pausedStateFactory.Create();
                return _pausedState;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
