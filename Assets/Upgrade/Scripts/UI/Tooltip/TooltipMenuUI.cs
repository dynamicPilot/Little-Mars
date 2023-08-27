using LittleMars.Common.Signals;
using LittleMars.Common;
using LittleMars.UI.LevelMenus;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;
using LittleMars.TooltipSystem;
using UnityEngine.UI;
using LittleMars.Signals;

namespace LittleMars.UI.Tooltip
{
    public class TooltipMenuUI : MonoBehaviour
    {
        //[Header("Buttons")]
        //[SerializeField] Button _hideButton;
        [Header("Panel")]
        [SerializeField] GameObject _panel;

        [SerializeField] TooltipPositionSetter _positionSetter;
        [SerializeField] TooltipTextSetter _textSetter;

        TooltipMenu _menu;
        bool _isOpen = false;
        [Inject]
        public void Constructor(TooltipMenu menu)
        {
            _menu = menu;
            if (_menu == null)
                Debug.Log("No TooltipMenu");
            _isOpen = false;
        }

        //void Init() => SetButtons();

        //void SetButtons()
        //{
        //    Debug.Log("Set buttons ");
        //    if (_hideButton == null) Debug.Log("Null button ");
        //    _hideButton.onClick.AddListener(CallForHideTooltip);
        //}

        //private void OnDestroy()
        //{
        //    _hideButton.onClick.RemoveAllListeners();
        //}

        public void OnOpenMenu(TooltipContext context)
        {
            if (!TrySetTooltip(context)) CallForHideTooltip();
            else Open();
        }

        public void OnCloseMenu()
        {            
            Close();
        }

        void CallForHideTooltip()
        {
            _menu.CallForHideTooltip();
        }

        bool TrySetTooltip(TooltipContext context)
        {
            return _positionSetter.TrySetTooltip(context);
        }

        void Open()
        {
            _isOpen = true;
            _menu.Open();
            _panel.SetActive(true);
        }

        void Close()
        {
            _isOpen = false;
            _panel.SetActive(false);
        }

        //protected override void Close()
        //{
        //    // signal
        //    _signalBus.TryFire(new WindowIsClosedSignal { MenuState = (int)MenuState.state });
        //    base.Close();
        //}
    }
}
