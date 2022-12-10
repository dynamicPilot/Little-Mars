using LittleMars.Common;
using LittleMars.Common.Interfaces;
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
        ViewSlotView _view;
        ViewSlotUI _viewUI;
        ViewSlot _data;
        ViewSlotState _state;
        IModelFacade _model;

        [Inject]
        public void Constructor(ViewSlotView view, ViewSlot data, ViewSlotState state,
            ViewSlotUI viewSlotUI, IModelFacade model)
        {
            _view = view;
            _viewUI = viewSlotUI;
            _data = data;
            _model = model;
            _state = state;
        }

        //public Vector2 Position()  => _view.Position;
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

        public class Factory : PlaceholderFactory<Vector2, ViewSlotFacade>
        {

        }
    }
}

