using LittleMars.Common;
using LittleMars.Loaders;

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
        public void Load() => _loader.LoadSceneAsync(_nextSceneType);
    }
}
