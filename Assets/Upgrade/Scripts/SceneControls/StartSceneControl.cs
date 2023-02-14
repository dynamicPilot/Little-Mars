using LittleMars.Loaders;
using UnityEngine;
using Zenject;

namespace LittleMars.SceneControls
{
    public class StartSceneControl : IInitializable
    {
        readonly SceneLoader _sceneLoader;
        public StartSceneControl(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            Debug.Log("StartSceneControl");
            _sceneLoader.LoadSceneAsync(Common.SceneType.menu, 
                UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
