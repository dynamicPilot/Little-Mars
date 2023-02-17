using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class LevelInfoSlotUI : SlotUI, IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI _number;
        [SerializeField] Image _isDoneSign;

        ISlotOnClick _onClick;
        int _levelIndex;
        private void OnValidate()
        {
            _isDoneSign.enabled = false;
        }

        public void SetLevelInfo(Common.Levels.LevelInfo info, ISlotOnClick onClick, bool isDone = false)
        {
            SetSlot(info.EndSprite);
            _isDoneSign.enabled = isDone;
            _number.text = info.Number.ToString();
            _levelIndex = info.Number - 1;

            _onClick = onClick;
        }

        public void SetEmpty()
        {
            _isDoneSign.enabled = false;
            _number.text = "";
            _sign.enabled = false;
            _levelIndex = -1;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_levelIndex == -1 || _onClick == null) return;
            _onClick.SlotOnClick(_levelIndex);
        }
    }
}
