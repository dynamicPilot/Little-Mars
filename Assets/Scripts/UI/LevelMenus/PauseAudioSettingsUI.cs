using UnityEngine;

namespace LittleMars.UI.LevelMenus
{
    public class PauseAudioSettingsUI : MonoBehaviour
    {
        [SerializeField] PauseMuteButtonUI _muteMusicButton;
        [SerializeField] PauseMuteButtonUI _muteAllButton;

        public void SetParameters()
        {
            _muteMusicButton.SetParameters();
            _muteAllButton.SetParameters();
        }

        public bool NeedSave()
        {
            return _muteAllButton.NeedSave() || _muteMusicButton.NeedSave();
        }

    }
}
