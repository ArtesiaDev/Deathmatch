using System.Threading.Tasks;
using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View;
using Scripts.Services.SceneLoader;
using Zenject;
using Scene = Scripts.Services.SceneLoader.Scene;

namespace _Project.Scripts.Core.Logic
{
    public class GamePanelPresenter
    {
        private GamePanelView _view;
        private GamePanelModel _model;
        private ISceneProvider _sceneProvider;

        [Inject]
        private void Construct(GamePanelView view, GamePanelModel model, ISceneProvider sceneProvider)
        {
            _view = view;
            _model = model;
            _sceneProvider = sceneProvider;
        }

        public async void CreateNewGame()
        {
            await _sceneProvider.LoadSceneSingle(Scene.Game);
        }

        public async void JoinExistingGame()
        {
            await _sceneProvider.LoadSceneAdditive(Scene.Game, true);

            await Task.Delay(50000);

            await _sceneProvider.ActivateScene(Scene.Game);

            await Task.Delay(50000);

            await _sceneProvider.UnloadScene(Scene.MainMenu, false);

            await Task.Delay(2000);

            await _sceneProvider.LoadSceneAdditive(Scene.MainMenu);

            await Task.Delay(2000);

            await _sceneProvider.RunLoadingScene(Scene.MainMenu);

            await Task.Delay(2000);

            await _sceneProvider.ActivateScene(Scene.MainMenu);

            await Task.Delay(2000);

            await _sceneProvider.UnloadScene(Scene.Game, false);
        }

        public void OnSessionCodeEndEdit(string code)
        {
            _model.SetCurrentSessionCode(code);
        }
    }
}