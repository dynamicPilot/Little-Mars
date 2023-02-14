using LittleMars.Common;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LittleMars.Loaders
{
    public class SceneLoader
    {
        readonly Settings _settings;

        public SceneLoader(Settings settings)
        {
            _settings = settings;
        }

        public void LoadSceneAsync(SceneType type, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var name = GetSceneName(type);
            Debug.Log($"Loading scene with name {name}");
            SceneManager.LoadSceneAsync(name, mode);
        }

        string GetSceneName(SceneType type)
        {
            return (type == SceneType.menu) ? _settings.MenuSceneName :
                _settings.LevelSceneName;
        }

        [Serializable]
        public class Settings
        {
            public string MenuSceneName;
            public string LevelSceneName;
        }
    }
}
