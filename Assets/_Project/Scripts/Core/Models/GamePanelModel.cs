namespace _Project.Scripts.Core.Models
{
    public class GamePanelModel
    {
        public string CurrentSessionCode { get; private set; }

        public void SetCurrentSessionCode(string code) =>
            CurrentSessionCode = code;
    }
}