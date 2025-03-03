using _Project.Scripts.Core.Logic.MainScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Core.View.MainScene
{
    public class GamePanelView : MonoBehaviour
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private TMP_InputField _sessionCode;

        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private GameObject _loadingIcon;
        [SerializeField] private Button _disconnectButton;

        [SerializeField] private GameObject _invalidPanel;
        [SerializeField] private GameObject _createGamePanel;
        [SerializeField] private GameObject _joinGamePanel;
        [SerializeField] private TextMeshProUGUI _invalidPanelText;
        [SerializeField] private Button _invalidOkButton;

        private GamePanelPresenter _presenter;

        [Inject]
        private void Construct(GamePanelPresenter presenter) =>
            _presenter = presenter;

        private void Awake()
        {
            _createButton.onClick.AddListener(_presenter.CreateNewGame);
            _joinButton.onClick.AddListener(_presenter.JoinExistingGame);
            _sessionCode.onEndEdit.AddListener(_presenter.OnSessionCodeEndEdit);
            _sessionCode.onValueChanged.AddListener(_presenter.OnSessionCodeValidate);
            _disconnectButton.onClick.AddListener(_presenter.DisconnectGame);
            _invalidOkButton.onClick.AddListener(_presenter.CloseInvalidPanel);
        }

        private void OnDestroy()
        {
            _sessionCode.onEndEdit.RemoveAllListeners();
            _createButton.onClick.RemoveAllListeners();
            _joinButton.onClick.RemoveAllListeners();
            _disconnectButton.onClick.RemoveAllListeners();
        }

        public void DrawSessionCode(string code) =>
            _sessionCode.text = code;

        public void SwitchLoadingPanelRender(bool predicate) =>
            _loadingPanel.SetActive(predicate);

        public void SwitchInvalidPanelRender(bool predicate, string invalidCode = default)
        {
            _invalidPanel.SetActive(predicate);
            _createGamePanel.SetActive(!predicate);
            _joinGamePanel.SetActive(!predicate);

            if (invalidCode != default)
                _invalidPanelText.text =
                    $"The Session Code \"{invalidCode}\" is not valid session code.\n\nPlease enter 8 characters or digit.";
        }

        public GameObject GetLoadingIcon() =>
            _loadingIcon;
    }
}