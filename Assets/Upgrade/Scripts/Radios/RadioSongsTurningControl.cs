using LittleMars.Settings;
using System.Collections;
using UnityEngine;

namespace LittleMars.UI.AudioSystems
{
    public class RadioSongsTurningControl : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        [SerializeField] SongsCatalogue _catalogue;

        WaitForSecondsRealtime _timer;
        int _songIndex;

        public void StartRadio()
        {
            _songIndex = -1;
            NextSong();
        }

        public void StopRadio()
        {
            StopAllCoroutines();
            _source.Stop();
        }

        void NextSong()
        {
            _songIndex++;
            if (_songIndex >= _catalogue.Songs.Length)
                _songIndex = 0;
            Play();
        }

        void ResetIndex()
        {
            _songIndex = 0;
            Play();
        }

        void Play()
        {
            var clip = _catalogue.Songs[_songIndex];

            if (clip == null)
            {
                ResetIndex();
                return;
            }

            StopAllCoroutines();
            _source.clip = clip;
            _timer = new WaitForSecondsRealtime(clip.length);
            StartCoroutine(PlaySingleSong());
        }

        IEnumerator PlaySingleSong()
        {
            _source.Play();
            yield return _timer;
            _source.Pause();
            NextSong();
        }
    }
}
