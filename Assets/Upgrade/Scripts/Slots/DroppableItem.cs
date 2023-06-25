using LittleMars.Common;
using LittleMars.Slots.States;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots
{
    public class DroppableItem : MonoBehaviour, IDropTarget
    {
        ViewSlotState _state;

        [Inject]
        public void Constructor(ViewSlotState state)
        {
            _state = state;
        }
        
        public void OnDrop(BuildingObject buildingObject)
        {
            //Debug.Log("Drop " + buildingObject.name);
            _state.OnDrop(buildingObject);
        }
    }
}
