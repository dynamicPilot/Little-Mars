using LittleMars.Common;
using LittleMars.UI.AudioSystems;
using LittleMars.UI.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LittleMars.UI.MainMenu
{
    public class MuteStateButton : PressedUIEffect
    {
        [SerializeField] Button _button;

        public Button Button { get => _button; }
        States _state = States.on;

        public void SetState(States state)
        {
            if (_state != state) _state = state;
            UpdateView();
        }

        public bool ChangeStateToOpposite()
        {
            _state = (_state == States.on) ? States.off : States.on;
            UpdateView();
            return (_state == States.on);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
        }

        void UpdateView()
        {
            Debug.Log($"SetState {_state}");
            if (_state == States.on) NormalState();
            else PressedState();
        }
    }
}
