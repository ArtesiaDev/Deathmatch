using _Project.Scripts.Core.Logic;
using _Project.Scripts.Core.View;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.SceneContext
{
    public class MainMenuInstaller: MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.Bind<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
        }
    }
}