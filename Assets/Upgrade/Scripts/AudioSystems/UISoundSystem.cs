using LittleMars.Common;
using LittleMars.UI.AudioSystems;

namespace LittleMars.AudioSystems
{
    public class UISoundSystem
    {
        readonly AudioSystemUI _systemUI;
        public UISoundSystem(AudioSystemUI systemUI)
        {
            _systemUI = systemUI;
        }
        public void PlayUISound(UISoundType type)
        {
            _systemUI.PlayUISound(type);
        }
    }
}
