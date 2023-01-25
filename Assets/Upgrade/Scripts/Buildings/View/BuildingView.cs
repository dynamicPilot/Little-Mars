using LittleMars.Buildings.BuildingStates;
using LittleMars.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingView : MonoBehaviour, IPointerClickHandler
    {
        BuildingObjectView _view;
        BuildingState _state;

        [Inject]
        public void Constructor(BuildingObjectView.Factory factory, BuildingState state)
        {
            _view = factory.Create();
            _state = state;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _state.OnClickPerformed();
        }

        public void SetViewActiveState(bool activeState)
        {
            _view.gameObject.SetActive(activeState);
        }

        public void OnView()
        {
            _view.TransitToState(BStates.on);
        }

        public void OffView()
        {
            _view.TransitToState(BStates.off);
        }

        public void PausedView()
        {
            _view.TransitToState(BStates.paused);
        }


    }
}
