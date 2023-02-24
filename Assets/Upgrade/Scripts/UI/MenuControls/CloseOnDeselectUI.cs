using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleMars.UI.MenuControls
{
    public class CloseOnDeselectUI : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        bool _isMouseOver = false;
        protected ICloseMenuUI _menuToClose;

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (!_isMouseOver)
                _menuToClose.CloseMenu();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseOver = true;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseOver = false;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
