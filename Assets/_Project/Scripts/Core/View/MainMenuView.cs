using _Project.Scripts.Core.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Core.View
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _gameButton;
        [SerializeField] private Button _settingsButton;

        [SerializeField] private GameObject _mainWindow;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _gamePanel;

        [SerializeField] private TextMeshProUGUI _version;
        [SerializeField] private TextMeshProUGUI _currentWindow;

        private MainMenuPresenter _presenter;

        [Inject]
        private void Construct(MainMenuPresenter presenter) =>
            _presenter = presenter;

        private void Awake()
        {
            _gameButton.onClick.AddListener(_presenter.OpenGamePanel);
            _settingsButton.onClick.AddListener(_presenter.OpenSettingsPanel);
        }

        private void OnDestroy()
        {
            _gameButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        public void SwitchMainWindowRender(bool predicate)
        {
            _mainWindow.SetActive(predicate);
            
            if (!predicate)
                _currentWindow.text = "Main Menu";
        }

        public void SwitchSettingsPanelRender(bool predicate)
        {
            _settingsPanel.SetActive(predicate);
            
            if (predicate)
                _currentWindow.text = "Settings";
        }

        public void SwitchGamePanelRender(bool predicate)
        {
            _gamePanel.SetActive(predicate);
            
            if (predicate)
                _currentWindow.text = "Game Panel";
        }

        public void DrawVersion(string version) =>
            _version.text = $"Version {version}";
    }
}