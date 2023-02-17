using LittleMars.Common.Signals;
using Zenject;

namespace LittleMars.SceneControls
{
    public class LevelSceneControl : SceneControl
    {
        public LevelSceneControl(ProjectSceneControl projectControl, SignalBus signalBus)
            : base(projectControl, signalBus)
        {

        }

        protected override void Subscribe()
        {
            _signalBus.Subscribe<EndSceneSignal>(Load);
        }

        protected override void Unsubscribe()
        {
            _signalBus?.TryUnsubscribe<EndSceneSignal>(Load);
        }

    }
}
