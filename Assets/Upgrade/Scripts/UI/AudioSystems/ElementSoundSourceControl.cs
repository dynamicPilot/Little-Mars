using LittleMars.AudioSystems;
using LittleMars.Common.Catalogues;
using Zenject;

namespace LittleMars.UI.AudioSystems
{
    public class ElementSoundSourceControl : AudioSourceControl
    {
        [Inject]
        public void Constructor(SoundsCatalogue catalogue)
        {
            base.BaseConstructor(catalogue);
        }
        protected override void UpdateClip()
        {
            var clip = _catalogue.GetUISound(_index);
            if (clip == null) return;
            _source.clip = clip;
        }
    }
}
