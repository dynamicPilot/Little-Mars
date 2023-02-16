using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.MainMenu
{
    public class LevelInfoSlotUI : SlotUI
    {
        [SerializeField] TextMeshProUGUI _number;
        [SerializeField] Image _isDoneSign;

        private void OnValidate()
        {
            _isDoneSign.enabled = false;
        }

        public void SetLevelInfo(Common.Levels.LevelInfo info, bool isDone = false)
        {
            SetSlot(info.EndSprite);
            _isDoneSign.enabled = isDone;
            _number.text = info.Number.ToString();
        }

        public void SetEmpty()
        {
            _isDoneSign.enabled = false;
            _number.text = "";
            _sign.enabled = false;
        }
    }
}
