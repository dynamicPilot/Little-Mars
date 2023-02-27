using LittleMars.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Buttons
{
    public class ButtonWithStateView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public Button Button { get => _button; }
        States _state = States.on;

        public void SetState(States state)
        {
            if (_state == state) return;
            _state = state;
            UpdateView();
        }

        public States ChangeStateToOpposite()
        {
            _state = (_state == States.on) ? States.off :
                States.on;
            UpdateView();
            return _state;
        }

        private void UpdateView()
        {

        }
    }
}
