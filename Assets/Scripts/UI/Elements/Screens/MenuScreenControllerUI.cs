using UnityEngine.UI;
using UnityEngine;
using Zenject;
using LittleMars.UI.Tooltip;

namespace LittleMars.UI.Elements.MenuScreens
{
    public class MenuScreenControllerUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] Button _button;
        [SerializeField] TooltipInfo _tooltipInfo;

        int _id;
        MenuScreenController _controller;

        [Inject]
        public void Constructor(MenuScreenController controller)
        {
            _controller = controller;
            _id = transform.GetSiblingIndex();
            Init();
        }

        void Init()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        void OnButtonClick()
        {
            _controller.CallText(_tooltipInfo.GetText(), _id);
        }
    }
}
