using _Project.Scripts.Core.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Core.View
{
    public class GamePanelView : MonoBehaviour
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private TMP_InputField _sessionCode;

        private GamePanelPresenter _presenter;

        [Inject]
        private void Construct(GamePanelPresenter presenter) =>
            _presenter = presenter;

        private void Awake()
        {
            _createButton.onClick.AddListener(_presenter.CreateNewGame);
            _joinButton.onClick.AddListener(_presenter.JoinExistingGame);
            _sessionCode.onEndEdit.AddListener(_presenter.OnSessionCodeEndEdit);
        }

        private void OnDestroy()
        {
            _sessionCode.onEndEdit.RemoveAllListeners();
            _createButton.onClick.RemoveAllListeners();
            _joinButton.onClick.RemoveAllListeners();
        }
    }
}