using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using System.Collections;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.AudioSystems
{
    public class RocketSoundSourceControl : MonoBehaviour
    {
        [SerializeField] AudioSource _source;

        SignalBus _signalBus;
        WaitForSeconds _timer;
        bool _isPlaying;

        [Inject]
        public void Constructor(SoundsCatalogue.Settings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _source.clip = settings.RocketLaunch;
            _timer = new WaitForSeconds(_source.clip.length);

            _isPlaying = false;

            Init();
        }

        void Init()
        {
            _signalBus.Subscribe<RocketLaunchSignal>(OnRocketLaunch);
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<RocketLaunchSignal>(OnRocketLaunch);
        }

        void OnRocketLaunch()
        {
            if (_isPlaying) return;

            StartCoroutine(Playing());
        }

        IEnumerator Playing()
        {
            _isPlaying = true;
            _source.Play();
            yield return _timer;
            _source.Stop();
            _isPlaying = false;
        }
    }
}
