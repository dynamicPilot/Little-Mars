using LittleMars.Common.Catalogues;
using UnityEngine;

namespace LittleMars.AudioSystems
{
    /// <summary>
    /// Base class for audio source control.
    /// </summary>
    public class AudioSourceControl : MonoBehaviour
    {
        [SerializeField] protected AudioSource _source;

        protected SoundsCatalogue _catalogue;
        protected int _index;
        protected void BaseConstructor(SoundsCatalogue catalogue)
        {
            _catalogue = catalogue;
            _index = -1;
        }
        public virtual void PlaySound(int typeIndex)
        {
            UpdateIndexAndClip(typeIndex);
            Play();
        }

        //public virtual void Stop()
        //{
        //    _source.Stop();
        //}

        //public virtual void Mute()
        //{
        //    _source.mute = true;
        //}

        protected void Play()
        {
            _source.Play();
        }

        void UpdateIndexAndClip(int typeIndex)
        {
            if (typeIndex == _index) return;

            _index = typeIndex;           
            UpdateClip();
        }

        protected virtual void UpdateClip()
        {
            
        }
    }
}
