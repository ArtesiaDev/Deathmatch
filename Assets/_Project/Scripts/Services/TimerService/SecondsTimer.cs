using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Services.TimerService
{
    public class SecondsTimer : Timer
    {
        private float _duration;
        public SecondsTimer(float duration) =>
            _duration = duration;

        public async UniTask WaitAsync()
        {
            while (!IsCompleted)
            {
                if (IsPaused)
                {
                    await UniTask.Yield();
                    continue;
                }

                var deltaTime = Time.deltaTime;
                if (deltaTime >= _duration)
                {
                    _duration = 0;
                    IsCompleted = true;
                    return;
                }

                _duration -= deltaTime;
                await UniTask.Yield();
            }
        }
    }
}