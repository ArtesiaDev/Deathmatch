using _Project.Scripts.Core.Logic.MainScene;
using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View.MainScene;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.SceneContext
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private GamePanelView _gamePanelView;

        public override void InstallBindings()
        {
            BindViews();
            BindLogic();
            BindModels();
        }

        private void BindViews()
        {
            Container.Bind<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
            Container.Bind<SettingsView>().FromInstance(_settingsView).AsSingle();
            Container.Bind<GamePanelView>().FromInstance(_gamePanelView).AsSingle();
        }

        private void BindLogic()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePanelPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BackendLoader>().AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<SettingsModel>().AsSingle();
            Container.Bind<GamePanelModel>().AsSingle();
        }
    }
}