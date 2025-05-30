using System;
using System.Threading.Tasks;
using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View.MainScene;
using _Project.Scripts.EventSignals;
using _Project.Scripts.Services.AudioManagement;
using DG.Tweening;
using Scripts.Core.GameEntity;
using Scripts.Services.SceneLoader;
using UnityEngine;
using WebSocketSharp;
using Zenject;
using AudioClip = _Project.Scripts.Services.AudioManagement.AudioClip;
using Scene = Scripts.Services.SceneLoader.Scene;

namespace _Project.Scripts.Core.Logic.MainScene
{
    public class GamePanelPresenter : IInitializable, IDisposable
    {
        private event Action MainMenuSceneUnloaded;

        private GamePanelView _view;
        private GamePanelModel _model;
        private ISceneProvider _sceneProvider;
        private ProjectSignals _signals;
        private BackendLoader _backendLoader;
        private AudioSystem _audioSystem;

        private GameObject _loadingIcon;
        private Tweener _loadingTween;

        [Inject]
        private void Construct(GamePanelView view, GamePanelModel model, ISceneProvider sceneProvider,
            ProjectSignals signals, BackendLoader loader, AudioSystem audioSystem)
        {
            _view = view;
            _model = model;
            _sceneProvider = sceneProvider;
            _signals = signals;
            _backendLoader = loader;
            _audioSystem = audioSystem;
        }

        public void Initialize()
        {
            _loadingIcon = _view.GetLoadingIcon();
            MainMenuSceneUnloaded += _signals.OnMainMenuSceneUnloaded;
        }

        public void Dispose() =>
            _loadingTween?.Kill();

        public async void CreateNewGame()
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickWhoosh);
            await TransitionToGameScene(async () => { await _backendLoader.SettingUpNewGame(); });
        }

        public async void JoinExistingGame()
        {
            if (_model.CurrentSessionCode.IsNullOrEmpty())
                return;

            _audioSystem.PlayOneShotSound(AudioClip.ClickWhoosh);
            
            if (_backendLoader.SessionCodeIsValid(_model.CurrentSessionCode))
                await TransitionToGameScene(async () => { await _backendLoader.ConnectJoinGame(); });

            else _view.SwitchInvalidPanelRender(true, _model.CurrentSessionCode);
        }

        public void OnSessionCodeEndEdit(string code)
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical);
            _model.SetCurrentSessionCode(code);
        }

        public void OnSessionCodeValidate(string code)
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical, 0.5f, 0.7f);
            if (code.Length > 8)
                _view.DrawSessionCode(code[..8]);
        }

        public async void DisconnectGame()
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickPunch, 0.25f);
            _model.CancellationToken.Cancel();
            await _sceneProvider.UnloadScene(Scene.Game, false, () =>
            {
                _loadingTween?.Kill();
                _loadingIcon.transform.rotation = Quaternion.identity;
                _view.SwitchLoadingPanelRender(false);
            });
        }

        public void CloseInvalidPanel()
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical);
            _view.SwitchInvalidPanelRender(false);
        }

        private async Task TransitionToGameScene(Func<Task> onBackendComplete = null)
        {
            await _sceneProvider.LoadSceneAdditive(Scene.Game, true);

            _view.SwitchLoadingPanelRender(true);
            _loadingTween = _loadingIcon.transform
                .DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutFlash)
                .SetLoops(-1, LoopType.Restart);

            if (onBackendComplete != null)
                await onBackendComplete.Invoke();

            if (_model.FinishTransitionPredicate == false)
                return;

            await _sceneProvider.ActivateScene(Scene.Game);
            await _sceneProvider.UnloadScene(Scene.MainMenu, false,
                () =>
                {
                    _audioSystem.StopBackgroundMusic(2.5f);
                    MainMenuSceneUnloaded?.Invoke();
                    MainMenuSceneUnloaded -= _signals.OnMainMenuSceneUnloaded;
                });
        }
    }
}