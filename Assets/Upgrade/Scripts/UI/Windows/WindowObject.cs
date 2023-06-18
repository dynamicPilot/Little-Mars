using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Windows
{
    public class WindowObject : MonoBehaviour
    {
        [SerializeField] MenuUI _menuUI;
        public void Open()
        {
            _menuUI.OnOpenMenu();
        }

        public void Close()
        {
            _menuUI.OnCloseMenu();
        }
        public class Factory : PlaceholderFactory<WindowObject>
        { }
    }
}
