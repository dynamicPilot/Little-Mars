using LittleMars.WindowManagers;
using UnityEngine;

namespace LittleMars.UI
{
    /// <summary>
    /// A base MenuUI. Supports only Open and Close calls for menu panel. 
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] protected GameObject _panel;

        protected bool _isOpen;
        protected WindowContext _context = null;
        protected virtual void Awake()
        {
            _isOpen = false;
        }

        public virtual void OnOpenMenu(WindowContext context)
        {
            _context = context;
            Open();
        }
        public virtual void OnCloseMenu() => Close();

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
