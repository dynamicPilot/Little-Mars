using LittleMars.Common;
using System;

namespace LittleMars.Buildings.States
{
    public class BuildingStateManager
    {
        readonly BuildingOnState.Factory _onStateFactory;
        readonly BuildingOffState.Factory _offStateFactory;

        IBuildingState _onState = null;
        IBuildingState _offState = null;

        public BuildingStateManager(BuildingOnState.Factory onStateFactory, 
            BuildingOffState.Factory offStateFactory)
        {
            _onStateFactory = onStateFactory;
            _offStateFactory = offStateFactory;
        }

        public IBuildingState CreateState(ProductionState state)
        {
            if (state == ProductionState.on)
            {
                if (_onState == null) _onState = _onStateFactory.Create();
                return _onState;
            }
            else if (state == ProductionState.off)
            {
                if (_offState == null) _offState = _offStateFactory.Create();
                return _offState;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }


}
