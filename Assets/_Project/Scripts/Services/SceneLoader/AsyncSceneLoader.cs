using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Scripts.Services.SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        public async Task<SceneInstance> LoadScene(Scene name, Action onLoaded = null)
        {
            var scene = await LoadFromAddressables(name);
            scene.ActivateAsync();
            onLoaded?.Invoke();
            return scene;
        }

        private async Task<SceneInstance> LoadFromAddressables(Scene name)
        {
            var handle = Addressables.LoadSceneAsync(name.ToString());
            return await handle.Task;
        }
    }
}