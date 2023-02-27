using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/NotSoundSettings")]
    public class NotSoundSettings : ScriptableObject
    {
        [SerializeField] AudioClip _achievement;
        [SerializeField] AudioClip _start;
        [SerializeField] AudioClip _end;
        [SerializeField] AudioClip _gameOver;
        [SerializeField] AudioClip _canNotDo;

        Dictionary<int, AudioClip> _sounds = null;

        void SetDictionary()
        {
            _sounds = new();
            _sounds.Add((int)NotSoundType.achievement, _achievement);
            _sounds.Add((int)NotSoundType.start, _start);
            _sounds.Add((int)NotSoundType.end, _end);
            _sounds.Add((int)NotSoundType.gameOver, _gameOver);
            _sounds.Add((int)NotSoundType.canNotDo, _canNotDo);
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
