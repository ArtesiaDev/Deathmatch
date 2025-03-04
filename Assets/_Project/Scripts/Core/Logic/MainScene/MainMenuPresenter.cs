using _Project.Scripts.Core.View.MainScene;
using _Project.Scripts.Services.AudioManagement;
using Scripts.Core.GameEntity;
using UnityEngine;
using Zenject;
using AudioClip = _Project.Scripts.Services.AudioManagement.AudioClip;

namespace _Project.Scripts.Core.Logic.MainScene
{
    public class MainMenuPresenter : IInitializable
    {
        private MainMenuView _view;
        private AudioSystem _audioSystem;

        [Inject]
        private void Construct(MainMenuView view, AudioSystem audioSystem)
        {
            _view = view;
            _audioSystem = audioSystem;
        }

        public void Initialize() =>
            InitView();

        public void OpenGamePanel()
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical);
            _view.SwitchMainWindowRender(true);
            _view.SwitchGamePanelRender(true);
            _view.SwitchSettingsPanelRender(false);
        }

        public void OpenSettingsPanel()
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical);
            _view.SwitchMainWindowRender(true);
            _view.SwitchGamePanelRender(false);
            _view.SwitchSettingsPanelRender(true);
        }

        private void InitView()
        {
           _audioSystem.PlayBackgroundMusic(AudioClip.Music1, 0.5f);
            _view.DrawVersion(Application.version);
            _view.SwitchMainWindowRender(false);
            _view.SwitchGamePanelRender(false);
            _view.SwitchSettingsPanelRender(false);
        }
    }
}