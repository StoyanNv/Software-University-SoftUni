namespace BookLibrary.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Core;

    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger<LogExecution> logger;
        private LoggerConfiguration logConfiguration;
        private Logger log;
        public ExceptionFilter(ILogger<LogExecution> logger)
        {
            this.logger = logger;   
            this.logConfiguration = new LoggerConfiguration();
            this.log = this.logConfiguration.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public void OnException(ExceptionContext context)
        {
            var information = $"Exception type {context.Exception.GetType()} and stack trace {context.Exception.StackTrace}";
            log.Error(information);
            logger.LogError(information);
        }
    }
}