using LittleMars.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Buttons
{
    public class ButtonWithStateView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public Button Button { get => _button; }
        ProductionState _state = ProductionState.on;

        public void SetState(ProductionState state)
        {
            if (_state == state) return;
            _state = state;
            UpdateView();
        }

        public void ChangeStateToOpposite()
        {
            _state = (_state == ProductionState.on) ? ProductionState.off :
                ProductionState.on;
            UpdateView();
        }

        private void UpdateView()
        {

        }
    }
}
