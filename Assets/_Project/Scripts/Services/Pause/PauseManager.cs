using System.Collections.Generic;

namespace Scripts.Services.Pause
{
    public class PauseManager: IPauseManager, IPauseRegister
    {
        private readonly List<IPauseHandler> _handlers = new();
        private bool _isPaused;

        public void Register(IPauseHandler handler) =>
            _handlers.Add(handler);

        public void UnRegister(IPauseHandler handler) =>
            _handlers.Remove(handler);

        public void SetGlobalPause(bool isPaused)
        {
            _isPaused = isPaused;
            
            foreach (var handler in _handlers)
                handler.SetPause(_isPaused);
        }
    }
}