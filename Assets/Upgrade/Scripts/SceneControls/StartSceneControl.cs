using LittleMars.Common.Signals;
using LittleMars.Loaders;
using UnityEngine;
using Zenject;

namespace LittleMars.SceneControls
{
    public class StartSceneControl : SceneControl
    {
        public StartSceneControl(ProjectSceneControl projectControl, SignalBus signalBus) 
            : base(projectControl, signalBus)
        {
            NextSceneType(Common.SceneType.menu);
        }

        protected override void Subscribe()
        {
            Debug.Log("StartSceneControl : subscribe");
            _signalBus.Subscribe<EndSceneSignal>(Load);
        }

        protected override void Unsubscribe()
        {
            _signalBus?.TryUnsubscribe<EndSceneSignal>(Load);
        }
    }
}
