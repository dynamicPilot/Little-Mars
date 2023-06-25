using LittleMars.Common;
using LittleMars.Loaders;
using UnityEngine;

namespace LittleMars.SceneControls
{
    public class ProjectSceneControl
    {
        readonly SceneLoader _loader;

        SceneType _nextSceneType;
        public ProjectSceneControl(SceneLoader loader)
        {
            _loader = loader;
        }
        public void NextSceneType(SceneType type) => _nextSceneType = type;
        public void Load()
        {
            Debug.Log("ProjectSceneControl: Load.Type " + _nextSceneType);
            _loader.LoadSceneAsync(_nextSceneType);
        }
    }
}
