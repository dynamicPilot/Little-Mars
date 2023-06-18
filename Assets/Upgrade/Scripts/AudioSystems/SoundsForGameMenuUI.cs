using LittleMars.Common;
using UnityEngine;

namespace LittleMars.AudioSystems
{
    public class SoundsForGameMenuUI
    {
        readonly UISoundSystem _audioSystem;

        public SoundsForGameMenuUI(UISoundSystem audioSystem)
        {
            _audioSystem = audioSystem;
        }

        public void PlaySoundForCommandType(CommandType type)
        {
            if (type == CommandType.start) SoundForStart();
            else if (type == CommandType.quit) SoundForQuit();
            else if (type == CommandType.turnPage) SoundForTurnPage();
            else SoundForDefault();
        }

        void SoundForStart()
        {
            _audioSystem.PlayUISound(UISoundType.clickSecond);
        }

        void SoundForQuit()
        {
            _audioSystem.PlayUISound(UISoundType.quit);
        }

        void SoundForDefault()
        {
            _audioSystem.PlayUISound(UISoundType.clickFirst);
        }

        void SoundForTurnPage()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);
        }
    }
}
