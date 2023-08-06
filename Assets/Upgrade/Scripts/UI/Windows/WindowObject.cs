using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace LittleMars.UI.Windows
{
    public class WindowObject : MonoBehaviour
    {
        [SerializeField] MenuUI _menuUI;
        public void Open(WindowContext context)
        {
            _menuUI.OnOpenMenu(context);
        }

        public void Close()
        {
            _menuUI.OnCloseMenu();
        }
        public class Factory : PlaceholderFactory<WindowObject>
        { }
    }
}
