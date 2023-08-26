using LittleMars.TooltipSystem;
using LittleMars.UI.Tooltip;
using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    public class TooltipObject : MonoBehaviour
    {
        [SerializeField] TooltipMenuUI _menuUI;
        public void Open(TooltipContext context)
        {
            _menuUI.OnOpenMenu(context);
        }

        public void Close()
        {
            _menuUI.OnCloseMenu();
        }
        public class Factory : PlaceholderFactory<TooltipObject>
        { }
    }
}
