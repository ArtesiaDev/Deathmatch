using _Project.Scripts.Core.Logic.MainScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Core.View.MainScene
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _maxPlayers;
        [SerializeField] private Slider _soundsSlider;
        [SerializeField] private Slider _musicSlider;

        private SettingsPresenter _presenter;

        [Inject]
        private void Construct(SettingsPresenter presenter) =>
            _presenter = presenter;

        private void Awake()
        {
            _maxPlayers.contentType = TMP_InputField.ContentType.IntegerNumber;
            _maxPlayers.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _maxPlayers.onEndEdit.AddListener(_presenter.OnMaxPlayersEndEdit);
            _maxPlayers.onValueChanged.AddListener(_presenter.OnMaxPlayersValueChanged);
            _soundsSlider.onValueChanged.AddListener(_presenter.OnSoundsSliderValueChanged);
            _musicSlider.onValueChanged.AddListener(_presenter.OnMusicSliderValueChanged);
        }

        private void OnDestroy()
        {
            _maxPlayers.onValueChanged.RemoveAllListeners();
            _maxPlayers.onEndEdit.RemoveAllListeners();
            _soundsSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }

        public void DrawMaxPlayers(int maxPlayers) =>
            _maxPlayers.text = maxPlayers.ToString();

        public bool MaxPlayerIsFocused() =>
            _maxPlayers.isFocused;

        public void DrawSliders(float sounds, float music)
        {
            _soundsSlider.value = sounds;
            _musicSlider.value = music;
        }
    }
}