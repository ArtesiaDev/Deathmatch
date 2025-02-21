namespace Scripts.Services.TimerService
{
    public abstract class Timer
    {
        protected bool IsPaused;
        protected bool IsCompleted;
        
        public void Pause() =>
            IsPaused = true;

        public void Resume() =>
            IsPaused = false;
    }
}