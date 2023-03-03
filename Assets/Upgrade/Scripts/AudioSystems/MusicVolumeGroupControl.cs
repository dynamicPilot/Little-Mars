using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.AudioSystems
{
    public class MusicVolumeGroupControl : VolumeGroupControl
    {
        [Inject] SignalBus _signalBus;

        public MusicVolumeGroupControl(VolumeGroupData data) 
            : base(data)
        {
        }

        public override void UpdateForcedIsMute(bool isMute)
        {
            Debug.Log($"Music: forced mute update {isMute}");
            if (isMute)
            {
                _forcedIsMute = !IsMute;
                if (_forcedIsMute)
                {
                    Debug.Log($"Music: forced mute");
                    Mute();
                }
            }
            else
            {
                _forcedIsMute = false;
                if (_forcedIsMute && !IsMute)
                {
                    Debug.Log($"Music: forced unmute");
                    UnMute();                   
                }
            }
        }

        protected override void Mute()
        {
            base.Mute();
            _signalBus?.Fire<MuteMusicSignal>();
        }

        protected override void UnMute()
        {
            base.UnMute();
            _signalBus?.Fire<UnmuteMusicSignal>();
        }

        public new class Factory : PlaceholderFactory<VolumeGroupData, MusicVolumeGroupControl>
        { }

    }
}
