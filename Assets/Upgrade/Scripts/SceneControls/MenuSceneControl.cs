using LittleMars.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.SceneControls
{
    public class MenuSceneControl : IInitializable
    {
        readonly SceneLoader _sceneLoader;

        public MenuSceneControl(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _sceneLoader.LoadSceneAsync(Common.SceneType.level);
        }
    }
}
