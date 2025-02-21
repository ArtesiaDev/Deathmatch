namespace Scripts.Services.Pause
{
    public interface IPauseRegister
    {
        void Register(IPauseHandler handler);
        void UnRegister(IPauseHandler handler);
    }
}