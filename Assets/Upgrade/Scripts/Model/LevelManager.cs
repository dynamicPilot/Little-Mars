using LittleMars.Common.Signals;
using Zenject;

namespace LittleMars.Model
{
    public class LevelManager : IInitializable
    {
        SignalBus _signalBus;

        public LevelManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartLevelSignal>(StartLevel);
        }

        private void StartLevel()
        {
            if (ReadyToStart())
                StartGame();
        }

        bool ReadyToStart()
        {
            return true;

        }

        void StartGame()
        {
            _signalBus.Fire<StartGameSignal>();
        }

        private void GameOver()
        {

        }

        private void EndGame()
        {

        }

        private void EndLevel()
        {

        }

    }
}
