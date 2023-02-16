using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/LevelsSettings")]
    public class LevelsSettings : ScriptableObject
    {
        [SerializeField] LevelSettings[] _levels;


        public bool HasLevel(int levelNumber)
        {
            var index = levelNumber - 1;
            return (index >= 0 && index < _levels.Length);
        }

        public LevelSettings GetLevel(int levelNumber)
        {
            if (!HasLevel(levelNumber)) return null;
            else return _levels[levelNumber - 1];
        }

        public LevelSettings[] GetLevels() => _levels;
    }
}
