using LittleMars.Common.Signals;
using Zenject;

namespace LittleMars.Commands
{
    public class LevelReceiver : Receiver
    {
        readonly SignalBus _signalBus;

        public LevelReceiver(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Next()
        {

        }
        public virtual void MainMenu()
        {

        }
        public virtual void Restart()
        {

        }
        public virtual void Start()
        {
            _signalBus.Fire<StartLevelSignal>();
        }
        public virtual void Menu()
        {

        }
        public virtual void Continue()
        {

        }
    }
}
