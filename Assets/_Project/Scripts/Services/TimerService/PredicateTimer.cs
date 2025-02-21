using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Services.TimerService
{
    public class PredicateTimer: Timer
    {
        private readonly Func<bool> _predicate;
        public PredicateTimer(Func<bool> predicate) =>
            _predicate = predicate;
        
        public async UniTask WaitAsync()
        {
            while (!IsCompleted)
            {
                if (IsPaused)
                {
                    await UniTask.Yield(); 
                    continue;
                }

                if (_predicate != null)
                {
                    if (_predicate.Invoke())
                    {
                        IsCompleted = true;
                        return;
                    }
                }
                await UniTask.Yield();
            }
        }
    }
}