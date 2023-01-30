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

        public void Start()
        {
            _signalBus.Fire<StartGameSignal>();
        }

    }
}
