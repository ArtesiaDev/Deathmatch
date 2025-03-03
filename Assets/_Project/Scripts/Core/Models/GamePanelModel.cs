using System.Threading;

namespace _Project.Scripts.Core.Models
{
    public class GamePanelModel
    {
        public string CurrentSessionCode { get; private set; }
        public bool FinishTransitionPredicate {get; private set;}
        public CancellationTokenSource CancellationToken { get; private set; } = new();

        public void SetCurrentSessionCode(string code) =>
            CurrentSessionCode = code;
        
        public void SetFinishTransitionPredicate(bool predicate) => 
            FinishTransitionPredicate = predicate;

        public void SetNewCancellationToken() =>
            CancellationToken = new CancellationTokenSource();
    }
}