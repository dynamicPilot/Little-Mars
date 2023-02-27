using LittleMars.Common;
using UnityEngine;

namespace LittleMars.UI.AudioSystems
{
    public class AudioSystemUI : MonoBehaviour
    {
        [SerializeField] ElementSoundSourceControl _sourceUIControl;

        public void PlayUISound(UISoundType type)
        {
            _sourceUIControl.PlaySound((int)type);
        }

        public ElementSoundSourceControl GetSourceForElementUI()
        {
            return _sourceUIControl;
        }

    }
}
