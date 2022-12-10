
using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots.States
{
    public class SlotWaitingState : ISlotState
    {
        public void Dispose()
        {
        }

        public void OnDrop(BuildingObject buildingObject)
        {
            return;
        }

        public void SetView()
        {
            Debug.Log("waiting state");
            return;
        }

        public class Factory : PlaceholderFactory<SlotWaitingState>
        {
        }
    }
}
