using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleMars.UI.Effects
{
    public class PressedUIEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] GameObject _normalState;
        [SerializeField] GameObject _pressedState;
        [SerializeField] bool _normalOnAwake = true;

        private void Awake()
        {
            if (_normalOnAwake) NormalState();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            PressedState();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            NormalState();
        }

        protected void NormalState()
        {
            _normalState.SetActive(true);
            _pressedState.SetActive(false);
        }

        protected void PressedState()
        {
            _normalState.SetActive(false);
            _pressedState.SetActive(true);
        }
    }
}
