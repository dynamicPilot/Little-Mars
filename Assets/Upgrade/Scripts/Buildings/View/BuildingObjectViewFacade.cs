using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectViewFacade : MonoBehaviour
    {
        [SerializeField] private BuildingObjectView _view;

        BuildingObjectViewStates _state;

        [Inject]
        public void Constructor(BuildingObjectViewStates state)
        {
            _state = state;
        }

        public void OnStart() => _view.OnStart();
        public void OnRemove() => _view.OnRemove();
        public void RotateView(float angle) => _view.RotateView(angle);
        public void TransitToState(BStates state) => _state.TransitToState(state);
        public void OnStartViewEffected() => _state.TransitToState(BStates.effected);
        public void OnEndViewEffected() => _state.OnEndEffected();

        public class Factory : PlaceholderFactory<BuildingObjectViewFacade>
        {
        }
    }
}
