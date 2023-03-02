using Newtonsoft.Json.Linq;
using UnityEngine;
using Zenject;

namespace LittleMars.AudioSystems
{
    public class VolumeGroupControl
    {
        public bool IsMute { get; private set; }

        VolumeGroupData _data;       
        float _volume;

        public VolumeGroupControl(VolumeGroupData data)
        {
            _data = data;
            ToDefault();
        }

        public void UpdateVolume(float value, bool needSet = true)
        {
            Debug.Log($"Update {_data.Parameter} volume {value}. CurrentVolume is {_volume}");
            _volume = value;
            var realValue = FractionToRealValue(value);
            if (needSet) _data.Mixer.SetFloat(_data.Parameter, realValue);
        }

        public void UpdateIsMute(bool isMute)
        {
            if (isMute) Mute();
            else UnMute();
        }

        public void UpdateViaConfig(float volume, bool isOn)
        {
            Debug.Log($"Update group via config {_data.Parameter}. Is on { isOn}.");
            UpdateVolume(volume, isOn);
            UpdateIsMute(!isOn);
        }

        public float ToDefault()
        {
            _volume = RealValueToFraction(_data.DefaultVolume);
            UnMute();
            return _volume;
            //_data.Mixer.SetFloat(_data.Parameter, _data.DefaultVolume);
        }

        public float GetVolume()
        {
            UpdateVolumeField();
            return _volume;
        }

        void UpdateVolumeField()
        {
            if (IsMute) return;

            _data.Mixer.GetFloat(_data.Parameter, out var value);
            _volume = RealValueToFraction(value);
        }

        void Mute()
        {
            Debug.Log($"Mute {_data.Parameter}");
            IsMute = true;
            _data.Mixer.SetFloat(_data.Parameter, _data.MinVolume);
        }

        void UnMute()
        {
            Debug.Log($"Unmute {_data.Parameter}");
            IsMute = false;
            _data.Mixer.SetFloat(_data.Parameter, _volume);
        }


        float FractionToRealValue(float value)
        {
            return Mathf.Lerp(_data.MinVolume, _data.MaxVolume, value);
        }

        float RealValueToFraction(float realValue)
        {
            return (realValue - _data.MinVolume) / (_data.MaxVolume - _data.MinVolume);
        }

        public class Factory : PlaceholderFactory<VolumeGroupData, VolumeGroupControl>
        { }
    }
}
