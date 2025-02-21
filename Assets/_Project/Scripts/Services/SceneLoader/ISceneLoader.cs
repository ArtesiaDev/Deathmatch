using System;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Scripts.Services.SceneLoader
{
    public interface ISceneLoader
    {
        public Task<SceneInstance> LoadScene(Scene scene, Action onLoaded = null);
    }
}