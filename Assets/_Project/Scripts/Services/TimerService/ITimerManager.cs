using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Services.TimerService
{
    public interface ITimerManager
    {
        UniTask WaitForSeconds(float duration);
        UniTask WaitForPredicate(Func<bool> predicate);
    }
}