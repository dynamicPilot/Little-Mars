using UnityEngine;

namespace LittleMars.UI
{
    /// <summary>
    /// A base MenuUI. Supports only Open and Close calls for menu panel. 
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        protected bool _isOpen;

        protected virtual void Awake()
        {
            _isOpen = false;
        }

        protected virtual void Open()
        {
            _panel.SetActive(true);
            _isOpen = true;
        }

        protected virtual void Close()
        {
            _panel.SetActive(false);
            _isOpen = false;
        }
    }
}
