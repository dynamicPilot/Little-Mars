using LittleMars.UI.AudioSystems;
using UnityEngine;
using Zenject;

namespace LittleMars.Radios
{
    public class RadioUI : MonoBehaviour
    {
        [SerializeField] RadioSongsTurningControl _control;

        public void StartRadio()
        {
            _control.StartRadio();
        }

        public void StopRadio()
        {
            _control.StopRadio();
        }

        public class Factory : PlaceholderFactory<RadioUI>
        { }
    }
}
