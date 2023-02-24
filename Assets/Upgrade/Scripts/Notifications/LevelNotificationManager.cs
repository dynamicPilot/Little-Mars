using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.Notifications
{
    public class LevelNotificationManager
    {
        readonly SignalBus _signalBus;

        NeedMenuInitSignal _signal;
        public void ResourceNotification(int resourceIndex)
        {
            
        }

        public void RouteNotification()
        {

        }

        void MakeNotification()
        {
            // signal to ui
            // signal to audio
        }
    }
}
