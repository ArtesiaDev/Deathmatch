using _Project.Scripts.Core.Logic.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.SceneContext
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameSceneLoader _gameSceneLoader;

        public override void InstallBindings()
        {
            Container.Bind<GameSceneLoader>().FromInstance(_gameSceneLoader).AsSingle();
        }
    }
}