using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.Model
{
    public class LevelManager
    {
        SignalBus _signalBus;

        public LevelManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void StartLevel()
        {

        }

        public void ReadyToStart()
        {

        }
        private void StartGame()
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
