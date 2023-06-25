using LittleMars.Common;
using Zenject;
using UnityEngine;

namespace LittleMars.SceneControls
{
    /// <summary>
    /// A base class for all scenes controls.
    /// </summary>
    public class SceneControl : IInitializable
    {
        readonly ProjectSceneControl _projectControl;
        protected readonly SignalBus _signalBus;

        public SceneControl(ProjectSceneControl projectControl, SignalBus signalBus)
        {
            _projectControl = projectControl;
            _signalBus = signalBus;
        }

        public void Initialize() => Subscribe();
        public void NextSceneType(SceneType type) => _projectControl.NextSceneType(type);

        protected void Load()
        {
            Debug.Log("SceneControl: Load.");
            Unsubscribe();
            _projectControl.Load();
        }

        protected virtual void Subscribe()
        {
        }

        protected virtual void Unsubscribe()
        {
        }
    }
}
