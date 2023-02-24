using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/LevelsSettings")]
    public class LevelsSettings : ScriptableObject
    {
        [SerializeField] LevelSettings[] _levels;

        public bool HasLevel(int levelIndex)
        {
            Debug.Log("Do we have level? " + levelIndex 
                 +" " + (levelIndex >= 0 && levelIndex < _levels.Length));
            return (levelIndex >= 0 && levelIndex < _levels.Length);
        }

        public LevelSettings GetLevel(int levelIndex)
        {
            if (!HasLevel(levelIndex))
            {
                Debug.Log("Null level to get");
                return null;
            }
            else
            {
                Debug.Log("Level with number to get: " + _levels[levelIndex].Info.LevelInfo.Number);
                return _levels[levelIndex];
            }
        }

        public LevelSettings[] GetLevels() => _levels;
    }
}
