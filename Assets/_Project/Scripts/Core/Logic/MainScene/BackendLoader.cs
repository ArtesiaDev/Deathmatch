using System;
using System.Threading.Tasks;
using _Project.Scripts.Core.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Logic.MainScene
{
    public class BackendLoader
    {
        private GamePanelModel _model;

        [Inject]
        private void Construct(GamePanelModel model) =>
            _model = model;
        
        public bool SessionCodeIsValid(string code)
        {
            return true;
        }

        public async Task ConnectJoinGame()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), _model.CancellationToken.Token);
            }
            catch (TaskCanceledException)
            {
                _model.SetNewCancellationToken();
                Debug.Log($"ConnectJoinGame task cancelled and create new Cancellation token: {_model.CancellationToken}");
                _model.SetFinishTransitionPredicate(false);
                return;
            }
            _model.SetFinishTransitionPredicate(true);
        }

        public async Task SettingUpNewGame()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2), _model.CancellationToken.Token);
            }
            catch (TaskCanceledException)
            {
                _model.SetNewCancellationToken();
                Debug.Log($"SettingUpNewGame task cancelled and create new Cancellation token: {_model.CancellationToken}");
                _model.SetFinishTransitionPredicate(false);
                return;
            }
            _model.SetFinishTransitionPredicate(true);
        }
    }
}