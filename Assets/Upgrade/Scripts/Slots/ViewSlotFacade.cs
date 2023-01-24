using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Connections;
using LittleMars.Connections.View;
using LittleMars.Slots.States;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


namespace LittleMars.Slots
{
    /// <summary>
    /// Main method for slot : keep state
    /// </summary>
    public class ViewSlotFacade : MonoBehaviour
    {
        [SerializeField] ViewSlotView _view;
        [SerializeField] ViewSlotUI _viewUI;
        [SerializeField] ConnectionIndicators _indicators;
        
        ViewSlot _data;
        ViewSlotState _state;
        IModelFacade _model;

        [Inject]
        public void Constructor(ViewSlot data, ViewSlotState state, IModelFacade model)
        {
            _data = data;
            _model = model;
            _state = state;
        }

        public void Indexes(int row, int columns) => _data.SetIndexes(row, columns);
        public void Resources(IEnumerable<Resource> resources) => _viewUI.UpdateSigns(resources.ToArray());
        public void TryAddBuilding(BuildingObject buildingObject)
        {
            _model.StartBuildingPlacement(buildingObject, _data.Indexes, _view.Position);
        }

        public void ChangeSlotStateTo(SlotStates state)
        {
            _state.ChangeState(state);
        }

        public void UpdateIndicators(Dictionary<Direction, Connection> connections)
        {
            _indicators.UpdateIndicators(connections);
        }

        public class Factory : PlaceholderFactory<Vector2, ViewSlotFacade>
        {

        }
    }
}

