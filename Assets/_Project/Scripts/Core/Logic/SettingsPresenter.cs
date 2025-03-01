using _Project.Scripts.Core.Models;
using _Project.Scripts.Core.View;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Logic
{
    public class SettingsPresenter: IInitializable
    {
        private SettingsView _view;
        private SettingsModel _model;

        [Inject]
        private void Construct(SettingsView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize() =>
            InitView();

        public void OnMaxPlayersEndEdit(string input)
        {
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

        public void OnSoundsSliderValueChanged(float percentage) =>
            _model.SetSoundsVolume(percentage);

        public void OnMusicSliderValueChanged(float percentage) =>
            _model.SetMusicVolume(percentage);

        private void InitView()
        {
           _view.DrawMaxPlayers(_model.MaxPlayers);
           _view.DrawSliders(_model.SoundsVolume, _model.MusicVolume);
        }
    }
}