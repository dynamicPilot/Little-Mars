namespace LittleMars.Map.States
{
    public class MapSlotCreatorStateFactory
    {
        readonly AutoMapSlotCreatorState.Factory _autoStateFactory;
        readonly CustomMapSlotCreatorState.Factory _customStateFactory;

        public MapSlotCreatorStateFactory(AutoMapSlotCreatorState.Factory autoStateFactory, 
            CustomMapSlotCreatorState.Factory customStateFactory)
        {
            _autoStateFactory = autoStateFactory;
            _customStateFactory = customStateFactory;
        }

        public IMapSlotCreatorState CreateState(MapMode state)
        {
            if (state == MapMode.auto)
                return _autoStateFactory.Create();
            else if (state == MapMode.custom)
                return _customStateFactory.Create();

            return null;
        }
    }
}
