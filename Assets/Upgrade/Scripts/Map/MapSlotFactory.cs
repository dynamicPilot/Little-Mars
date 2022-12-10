using LittleMars.Map.States;
using System.Collections.Generic;

namespace LittleMars.Map
{
    public class MapSlotFactory
    {
        MapSlotCreatorStateFactory _stateFactory;
        public MapSlotFactory(MapSlotCreatorStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public List<List<MapSlotExtended>> CreateSlots(MapMode mode)
        {
            using (_stateFactory.CreateState(mode))
            {
                var state = _stateFactory.CreateState(mode);
                return state.Create();
            }
                
        }

    }
}
