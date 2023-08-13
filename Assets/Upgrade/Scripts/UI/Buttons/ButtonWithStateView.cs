using LittleMars.Common;
using LittleMars.UI.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LittleMars.UI.Buttons
{
    public class ButtonWithStateView : PressedUIEffect
    {
        [SerializeField] Button _button;
        [SerializeField] bool _updateOnClick = false;

        public Button Button { get => _button; }
        States _state = States.on;

        public void SetState(States state)
        {
            if (_state != state) _state = state;
            //if (!_updateOnClick) UpdateView();
            UpdateView();
        }

        public States ChangeStateToOpposite()
        {
            _state = (_state == States.on) ? States.off : States.on;
            if (_updateOnClick) UpdateView();
            return _state;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
        }

        void UpdateView()
        {
            Debug.Log("Button: update view");
            if (_state == States.on) NormalState();
            else PressedState();
        }
    }
}
