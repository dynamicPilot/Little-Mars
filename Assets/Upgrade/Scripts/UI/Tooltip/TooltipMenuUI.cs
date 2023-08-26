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
        [Header("Buttons")]
        [SerializeField] Button _hideButton;
        [Header("Panel")]
        [SerializeField] GameObject _panel;

        [SerializeField] TooltipPositionSetter _positionSetter;
        [SerializeField] TooltipTextSetter _textSetter;

        TooltipMenu _menu;
        [Inject]
        public void Constructor(TooltipMenu menu)
        {
            _menu = menu;
            if (_menu == null)
                Debug.Log("No TooltipMenu");
            Init();
        }

        void Init() => SetButtons();

        void SetButtons()
        {
            Debug.Log("Set buttons ");
            if (_hideButton == null) Debug.Log("Null button ");
            _hideButton.onClick.AddListener(CallForHideTooltip);
        }

        private void OnDestroy()
        {
            _hideButton.onClick.RemoveAllListeners();
        }

        public void OnOpenMenu(TooltipContext context)
        {
            if (!TrySetTooltip(context)) CallForHideTooltip();
            else _panel.SetActive(true);
        }

        public void OnCloseMenu()
        {
            _panel.SetActive(false);
        }

        void CallForHideTooltip()
        {
            _menu.CallForHideTooltip();
        }

        bool TrySetTooltip(TooltipContext context)
        {
            return _positionSetter.TrySetTooltip(context);
        }

        //protected override void Close()
        //{
        //    // signal
        //    _signalBus.TryFire(new WindowIsClosedSignal { MenuState = (int)MenuState.state });
        //    base.Close();
        //}
    }
}
