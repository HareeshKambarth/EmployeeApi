namespace EmpApi.Logging
{
    public interface ILoggingService
    {
        void LogInformation(string message, Guid activityId);
        void LogError(string message, Exception ex, Guid activityId);
    }
}
