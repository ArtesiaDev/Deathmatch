using System;
using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View.MainScene;
using _Project.Scripts.Services.AudioManagement;
using UnityEngine;
using Zenject;
using AudioClip = _Project.Scripts.Services.AudioManagement.AudioClip;

namespace _Project.Scripts.Core.Logic.MainScene
{
    public class SettingsPresenter : IInitializable
    {
        private SettingsView _view;
        private SettingsModel _model;
        private AudioSystem _audioSystem;

        private bool _isInitialized;

        [Inject]
        private void Construct(SettingsView view, SettingsModel model, AudioSystem audioSystem)
        {
            _view = view;
            _model = model;
            _audioSystem = audioSystem;
        }

        public void Initialize() =>
            InitView();

        public void OnMaxPlayersEndEdit(string input)
        {
            _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical);
            if (string.IsNullOrEmpty(input))
            {
                _model.SetMaxPlayers(_model.MaxPlayersRange.Min);
                _view.DrawMaxPlayers(_model.MaxPlayers);
            }
            else
            {
                if (int.TryParse(input, out var value))
                {
                    var number = Mathf.Clamp(value, _model.MaxPlayersRange.Min, _model.MaxPlayersRange.Max);
                    _model.SetMaxPlayers(number);
                    _view.DrawMaxPlayers(_model.MaxPlayers);
                }
                else Debug.LogError($"Invalid number: {input}");
            }
        }

        public void OnMaxPlayersValueChanged(string code)
        {
            if (_isInitialized && _view.MaxPlayerIsFocused())
                _audioSystem.PlayOneShotSound(AudioClip.ClickMechanical, 0.5f, 0.7f);
            
            if (code.Length > 8)
                _view.DrawMaxPlayers(Convert.ToInt32(code[..8]));
        }

        public void OnSoundsSliderValueChanged(float percentage)
        {
            _model.SetSoundsVolume(percentage);
            _audioSystem.SetSoundsVolume(_model.SoundsVolume);
        }

        public void OnMusicSliderValueChanged(float percentage)
        {
            _model.SetMusicVolume(percentage);
            _audioSystem.SetMusicVolume(_model.MusicVolume);
        }

        private void InitView()
        {
            _view.DrawMaxPlayers(_model.MaxPlayers);
            _view.DrawSliders(_model.SoundsVolume, _model.MusicVolume);
            _isInitialized = true;
        }
    }
}