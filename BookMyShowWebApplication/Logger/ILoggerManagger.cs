namespace BookMyShowWebApplication.Logger
{
    public interface ILoggerManagger
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
