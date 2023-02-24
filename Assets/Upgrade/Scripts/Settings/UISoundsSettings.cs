using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/UISoundsSettings")]
    public class UISoundsSettings : ScriptableObject
    {
        [SerializeField] AudioClip _clickFirst;
        [SerializeField] AudioClip _clickSecond;
        [SerializeField] AudioClip _clickThird;
        [SerializeField] AudioClip _drop;
        [SerializeField] AudioClip _destroy;
        [SerializeField] AudioClip _turnOn;
        [SerializeField] AudioClip _turnOff;
        [SerializeField] AudioClip _quit;

        Dictionary<int, AudioClip> _sounds = null;

        void SetDictionary()
        {
            _sounds = new();
            _sounds.Add((int)UISoundType.clickFirst, _clickFirst);
            _sounds.Add((int)UISoundType.clickSecond, _clickSecond);
            _sounds.Add((int)UISoundType.clickThird, _clickThird);
            _sounds.Add((int)UISoundType.drop, _drop);
            _sounds.Add((int)UISoundType.destroy, _destroy);
            _sounds.Add((int)UISoundType.turnOn, _turnOn);
            _sounds.Add((int)UISoundType.turnOff, _turnOff);
            _sounds.Add((int)UISoundType.quit, _quit);
        }

        public AudioClip GetSound(int index)
        {
            if (_sounds == null) SetDictionary();

            if (_sounds.ContainsKey(index))
                return _sounds[index];
            else return _sounds[0];
        }
    }
}
