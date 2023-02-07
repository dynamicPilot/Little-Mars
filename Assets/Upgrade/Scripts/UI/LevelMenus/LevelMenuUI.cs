using LittleMars.Common;
using LittleMars.Common.Levels;
using LittleMars.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class LevelMenuUI : GameMenuUI
    {
        [Header("UI Elements")]
        [Header("Image")]
        [SerializeField] private MenuMode _imageMode;
        [SerializeField] private Image _image;
        [Header("Header")]
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private TextTagElement _headerText;
        // Level text -> need language control

        protected SignalBus _signalBus;
        Common.Levels.LevelInfo _info;

        protected override void Awake()
        {
            base.Awake();
        }

        protected void BaseConstructor(LevelMenu levelMenu, SignalBus signalBus, 
            Common.Levels.LevelInfo levelInfo)
        {
            _levelMenu = levelMenu;
            _signalBus = signalBus;
            _info = levelInfo;

            Init();
        }

        protected virtual void Init()
        {
        }

        public void SetMenu()
        {
            _levelNumberText.text = _info.Number.ToString();
            _image.sprite = (_imageMode == MenuMode.start) ? _info.StartSprite : _info.EndSprite;
            _headerText.SetText();
        }

        protected override void Close()
        {
            base.Close();
            RemoveListeners();
        }
    }
}
