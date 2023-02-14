using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleMars.UI.Effects
{
    public class PressedUIEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject _normalState;
        [SerializeField] private GameObject _pressedState;

        private void Awake()
        {
            NormalState();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("PRESSED!");
            PressedState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            NormalState();
        }

        private void NormalState()
        {
            _normalState.SetActive(true);
            _pressedState.SetActive(false);
        }

        private void PressedState()
        {
            _normalState.SetActive(false);
            _pressedState.SetActive(true);
        }
    }
}
