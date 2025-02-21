using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Scripts.Services.Pause;
using Zenject;

namespace Scripts.Services.TimerService
{
    public class TimerManager : IInitializable, IDisposable, IPauseHandler, ITimerManager
    {
        private readonly List<Timer> _activeTimers = new();

        private IPauseRegister _pauseRegister;
        private bool _isPaused;

        [Inject]
        private void Construct(IPauseRegister pauseRegister) =>
            _pauseRegister = pauseRegister;

        public void Initialize() =>
            _pauseRegister.Register(this);

        public void Dispose()
        {
            _pauseRegister.UnRegister(this);
            _activeTimers.Clear();
        }

        public void SetPause(bool isPause)
        {
            _isPaused = isPause;

            if (_isPaused)
                foreach (var timer in _activeTimers)
                    timer.Pause();
            else
                foreach (var timer in _activeTimers)
                    timer.Resume();
        }

        public async UniTask WaitForSeconds(float duration)
        {
            var timer = new SecondsTimer(duration);
            _activeTimers.Add(timer);
            await timer.WaitAsync();
            _activeTimers.Remove(timer);
        }

        public async UniTask WaitForPredicate(Func<bool> predicate)
        {
            var timer = new PredicateTimer(predicate);
            _activeTimers.Add(timer);
            await timer.WaitAsync();
            _activeTimers.Remove(timer);
        }
    }
}