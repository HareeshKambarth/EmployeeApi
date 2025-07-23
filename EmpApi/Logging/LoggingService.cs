namespace EmpApi.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, Guid activityId)
        {
            _logger.LogInformation("{Message} | ActivityId: {ActivityId}", message, activityId);
        }

        public void LogError(string message, Exception ex, Guid activityId)
        {
            _logger.LogError(ex, "{Message} | ActivityId: {ActivityId}", message, activityId);
        }
    }
}
