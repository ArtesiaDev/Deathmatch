using _Project.Scripts.Core.View;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Logic
{
    public class MainMenuPresenter: IInitializable
    {
        private MainMenuView _view;

        [Inject]
        private void Construct(MainMenuView view) 
            => _view = view;

        public void Initialize() =>
            InitView();

        public void OpenGamePanel()
        {
           _view.SwitchMainWindowRender(true);
           _view.SwitchGamePanelRender(true);
           _view.SwitchSettingsPanelRender(false);
        }

        public void OpenSettingsPanel()
        {
            _view.SwitchMainWindowRender(true);
            _view.SwitchGamePanelRender(false);
            _view.SwitchSettingsPanelRender(true);
        }

        private void InitView() =>
            _view.DrawVersion(Application.version);
    }
}