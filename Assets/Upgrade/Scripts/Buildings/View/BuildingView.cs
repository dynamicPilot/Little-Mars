using LittleMars.Buildings.BuildingStates;
using LittleMars.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingView : MonoBehaviour, IPointerClickHandler
    {
        BuildingObjectViewFacade _view;
        BuildingState _state;

        float _angle = 0f;

        [Inject]
        public void Constructor(BuildingObjectViewFacade.Factory factory, BuildingState state)
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
            if (_view.gameObject.activeSelf != activeState)
                _view.gameObject.SetActive(activeState);

            if (activeState) _view.OnStart();
            else _view.OnRemove();
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

        public void Rotate(float angle)
        {
            _angle = angle;
            transform.Rotate(0f, 0f,  -1 * angle);
            _view.RotateView(angle);
        }

        public void ResetView()
        {
            if (_angle == 0f) return;

            Rotate(-1 * _angle);
            //transform.rotation = Quaternion.identity;
            //_view.RotateView(-1 * _angle);
            //_angle = 0f;
        }
    }
}
