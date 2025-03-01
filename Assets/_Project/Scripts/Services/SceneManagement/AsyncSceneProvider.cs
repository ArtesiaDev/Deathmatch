using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;
using UScene = UnityEngine.SceneManagement.Scene;

namespace Scripts.Services.SceneLoader
{
    public class AsyncSceneProvider : ISceneProvider, IInitializable, IDisposable
    {
        private readonly Dictionary<Scene, SceneInstance> _loadedScenes = new();
        private readonly CancellationTokenSource _cancellationToken = new();
        private UScene _startScene;

        public void Initialize() =>
            _startScene = SceneManager.GetActiveScene();

        public void Dispose() =>
            _cancellationToken.Cancel();

        public async Task LoadSceneSingle(Scene name, Action onLoaded = null)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return;
            
            if (!_loadedScenes.TryGetValue(name, out var loadScene))
            {
                loadScene = await LoadFromAddressables(name, LoadSceneMode.Single, true);
                _loadedScenes.TryAdd(name, loadScene);
                onLoaded?.Invoke();
            }
        }

        public async Task LoadSceneAdditive(Scene name, bool runSceneImmediately = false, bool activateSceneImmediately = false,
            Action onLoaded = null)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return;
            
            if (!_loadedScenes.TryGetValue(name, out var loadScene))
            {
                loadScene = await LoadFromAddressables(name, LoadSceneMode.Additive, runSceneImmediately);
                _loadedScenes.TryAdd(name, loadScene);
                onLoaded?.Invoke();
            }

            if (activateSceneImmediately)
                await ActivateScene(name);
        }

        public async Task RunLoadingScene(Scene name)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return;
            
            if (_loadedScenes.TryGetValue(name, out var loadScene))
                await loadScene.ActivateAsync();

            else Debug.LogError($"Scene {name} is not loaded");
        }

        public async Task ActivateScene(Scene name, Action onActivated = null)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return;
            
            if (_loadedScenes.TryGetValue(name, out var loadScene))
            {
                await loadScene.ActivateAsync();
                SceneManager.SetActiveScene(loadScene.Scene);
                onActivated?.Invoke();
            }
            else if (_startScene.name == name.ToString())
                SceneManager.SetActiveScene(_startScene);

            else Debug.LogError($"Scene {name} is not loaded");
        }

        public async Task UnloadScene(Scene name, bool releaseHandle = false)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return;
            
            if (_loadedScenes.TryGetValue(name, out var scene))
            {
                await Addressables.UnloadSceneAsync(scene, releaseHandle);
                _loadedScenes.Remove(name);
            }
            else if (_startScene.name == name.ToString() && _startScene.isLoaded)
                await SceneManager.UnloadSceneAsync(_startScene);
            
            else Debug.LogError($"Scene {name} is not loaded");
        }

        private async Task<SceneInstance> LoadFromAddressables(Scene name, LoadSceneMode loadSceneMode, bool runScene)
        {
            if (_cancellationToken.Token.IsCancellationRequested)
                return default;
            
            var handle = Addressables.LoadSceneAsync(name.ToString(), loadSceneMode, runScene);
            return await handle.Task;
        }
    }
}