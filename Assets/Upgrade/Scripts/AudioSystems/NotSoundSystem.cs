using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.AudioSystems;
using System;
using Zenject;

namespace LittleMars.AudioSystems
{
    /// <summary>
    /// Class for notification sound system.
    /// </summary>
    public class NotSoundSystem : IInitializable, IDisposable
    {
        readonly NotSoundSourceControl _source;
        readonly SignalBus _signalBus;

        public NotSoundSystem(NotSoundSourceControl source, SignalBus signalBus)
        {
            _source = source;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStart);
            _signalBus.Subscribe<EndGameSignal>(OnEnd);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<CallAchivementMenuSignal>(OnAchievement);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<GoalStrategiesIsReadySignal>(OnStart);
            _signalBus?.TryUnsubscribe<EndGameSignal>(OnEnd);
            _signalBus?.TryUnsubscribe<GameOverSignal>(OnGameOver);
            _signalBus?.TryUnsubscribe<CallAchivementMenuSignal>(OnAchievement);
        }

        void PlayNotSound(NotSoundType type)
        {
            _source.PlaySound((int)type);
        }

        void OnStart()
        {
            PlayNotSound(NotSoundType.start);
        }

        void OnEnd()
        {
            PlayNotSound(NotSoundType.end);
        }

        void OnGameOver()
        {
            PlayNotSound(NotSoundType.gameOver);
        }

        void OnAchievement()
        {
            PlayNotSound(NotSoundType.achievement);
        }


    }
}
