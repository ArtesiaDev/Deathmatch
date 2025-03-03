using _Project.Scripts.Configs;
using _Project.Scripts.Core.Models;
using _Project.Scripts.EventSignals;
using _Project.Scripts.Services.AudioManagement;
using Scripts.Services.AssetManagement;
using Scripts.Services.Pause;
using Scripts.Services.SceneLoader;
using Scripts.Services.TimerService;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.ProjectContext
{
    public class ProjectInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _audioSystem;
        [SerializeField] private AudioConfig _audioConfig;

        public override void InstallBindings()
        {
           BindServices();
           BindProjectContextModels();
           BindPrefabs();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<AsyncSceneProvider>().AsSingle();
            Container.BindInterfacesTo<PauseManager>().AsSingle();
            Container.BindInterfacesTo<TimerManager>().AsSingle();
            Container.Bind<ProjectSignals>().AsSingle();
        }

        private void BindProjectContextModels()
        {
            Container.Bind<AudioModel>().AsSingle().WithArguments(_audioConfig);
        }

        private void BindPrefabs()
        {
            Container.Bind<AudioSystem>()
                .FromComponentInNewPrefab(_audioSystem)
                .AsSingle()
                .NonLazy();
        }
    }
}