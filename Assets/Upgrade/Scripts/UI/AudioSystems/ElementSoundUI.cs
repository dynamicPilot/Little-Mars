using LittleMars.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleMars.UI.AudioSystems
{
    public class ElementSoundUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] UISoundType _soundType;
        [SerializeField] AudioSystemUI _systemUI;

        ElementSoundSourceControl _sourceControl;

        private void Awake()
        {
            _sourceControl = _systemUI.GetSourceForElementUI();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _sourceControl.PlaySound((int)_soundType);
        }
    }
}
