using System;

namespace _Project.Scripts.EventSignals
{
    public class ProjectSignals
    {
        public event Action MainMenuSceneUnloaded;
        
        public void OnMainMenuSceneUnloaded() =>
            MainMenuSceneUnloaded?.Invoke();
    }
}