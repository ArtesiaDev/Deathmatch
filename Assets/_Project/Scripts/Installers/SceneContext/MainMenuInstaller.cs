using _Project.Scripts.Core.Logic.MainScene;
using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View.MainScene;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.SceneContext
{
    public class MainMenuInstaller: MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private GamePanelView _gamePanelView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.Bind<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
            
            Container.BindInterfacesAndSelfTo<SettingsPresenter>().AsSingle();
            Container.Bind<SettingsView>().FromInstance(_settingsView).AsSingle();
            
            Container.Bind<SettingsModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GamePanelPresenter>().AsSingle();
            Container.Bind<GamePanelView>().FromInstance(_gamePanelView).AsSingle();
            
            Container.Bind<GamePanelModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BackendLoader>().AsSingle();
        }
    }
}