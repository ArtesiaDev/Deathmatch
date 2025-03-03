using _Project.Scripts.EventSignals;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Logic.Game
{
    public class GameSceneLoader: MonoBehaviour
    {
        [SerializeField] private GameObject _eventSystem;
        [SerializeField] private GameObject _mainCamera;
        
        private ProjectSignals _projectSignals;
        
        [Inject]
        private void Construct(ProjectSignals projectSignals) =>
            _projectSignals = projectSignals;

        
        public void Awake() =>
            _projectSignals.MainMenuSceneUnloaded += OnMainMenuUnloaded;
        
        public void OnDestroy() =>
            _projectSignals.MainMenuSceneUnloaded -= OnMainMenuUnloaded;

        private void OnMainMenuUnloaded()
        {
            _eventSystem.SetActive(true);
            _mainCamera.SetActive(true);
        }
    }
}