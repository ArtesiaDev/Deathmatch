using System;
using System.Threading.Tasks;

namespace Scripts.Services.SceneLoader
{
    public interface ISceneProvider
    {
        Task LoadSceneSingle(Scene name, Action onLoaded = null);
        Task LoadSceneAdditive(Scene name, bool runSceneImmediately = false, bool activateSceneImmediately = false, Action onLoaded = null);
        Task RunLoadingScene(Scene name);
        Task ActivateScene(Scene name, Action onActivated = null);
        Task UnloadScene(Scene name, bool releaseHandle);
    }
}