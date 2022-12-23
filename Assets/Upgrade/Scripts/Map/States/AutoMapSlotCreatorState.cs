using Zenject;

namespace LittleMars.Map.States
{
    public class AutoMapSlotCreatorState : MapSlotCreatorState, IMapSlotCreatorState
    {
        public AutoMapSlotCreatorState(MapManager.Settings mapSettings,FieldManager fieldManager) 
            : base (mapSettings, fieldManager)
        {
        }

        public void Dispose()
        {            
        }

        public class Factory : PlaceholderFactory<AutoMapSlotCreatorState>
        {
        }
    }
}
