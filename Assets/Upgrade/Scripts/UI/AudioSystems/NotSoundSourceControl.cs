using LittleMars.AudioSystems;
using LittleMars.Common.Catalogues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.UI.AudioSystems
{
    public class NotSoundSourceControl : AudioSourceControl
    {
        [Inject]
        public void Constructor(SoundsCatalogue catalogue)
        {
            base.BaseConstructor(catalogue);
        }
        protected override void UpdateClip()
        {
            var clip = _catalogue.GetNotSound(_index);
            if (clip == null) return;
            _source.clip = clip;
        }
    }
}
