using System;
using System.Threading.Tasks;

namespace LittleMars.UI.LevelMenus
{
    public class AsyncSignalGunTimer
    {
        public void StartTimer(int delay, ISignalGun signalGun)
        {
            var result = DelayAsync(new TimeSpan(0, 0, delay), signalGun);
        }
        async Task DelayAsync(TimeSpan delay, ISignalGun signalGun)
        {
            await Task.Delay(delay);

            signalGun.FireSignal();
        }
    }
}
