using LittleMars.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Buttons
{
    public class ButtonWithStateView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public Button Button { get => _button; }
        ProductionState _state;

        public void SetState(ProductionState state)
        {
            _state = state;
        }
    }
}
