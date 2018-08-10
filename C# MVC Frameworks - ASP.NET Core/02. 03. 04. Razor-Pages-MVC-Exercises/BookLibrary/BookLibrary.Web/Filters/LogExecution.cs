namespace BookLibrary.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Core;
    using System;
    using System.Diagnostics;

    public class LogExecution : IPageFilter, IActionFilter
    {
        private ILogger<LogExecution> logger;
        private Stopwatch stopwatch;
        private LoggerConfiguration logConfiguration;
        private Logger log;
        public LogExecution(ILogger<LogExecution> logger, Stopwatch stopwatch, LoggerConfiguration logConfiguration)
        {
            this.logger = logger;
            this.stopwatch = stopwatch;
            this.logConfiguration = logConfiguration;
            this.log = this.logConfiguration.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.LogEnteringMessage(
                context.HttpContext.Request.Method,
                context.Controller.ToString().Substring(context.Controller.ToString().LastIndexOf('.') + 1),
                context.ActionDescriptor
                    .DisplayName
                    .Substring(context
                                   .Controller
                                   .ToString()
                                   .LastIndexOf('.') + 1)
                    .Split(new[] { '.', '(' }, StringSplitOptions.RemoveEmptyEntries)[1],
                context.ModelState.IsValid);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.LogLeavingMessage(
                context.HttpContext.Request.Method,
                context.Controller.ToString().Substring(context.Controller.ToString().LastIndexOf('.') + 1),
                context.ActionDescriptor
                    .DisplayName
                    .Substring(context
                             .Controller
                             .ToString()
                             .LastIndexOf('.') + 1)
                    .Split(new[] { '.', '(' }, StringSplitOptions.RemoveEmptyEntries)[1],
                context.ModelState.IsValid);
        }
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
          
            this.LogEnteringMessage(
                context.HttpContext.Request.Method,
                context.ActionDescriptor.DisplayName,
                context.HandlerMethod.MethodInfo.Name,
                context.ModelState.IsValid);
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            this.LogLeavingMessage(
                context.HttpContext.Request.Method,
                context.ActionDescriptor.DisplayName,
                context.HandlerMethod.MethodInfo.Name,
                context.ModelState.IsValid);
        }
        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { /*We don't need to handle this method*/ }

        private void LogEnteringMessage(string httpMethod, string controllerName, string actionName, bool isModelStateValid)
        {
            this.log.Information($"Executing <{httpMethod}><{controllerName}>.<{actionName}>");
            this.log.Information($"Model state: {(isModelStateValid ? "" : "in")}valid");

            logger.LogInformation($"Executing <{httpMethod}><{controllerName}>.<{actionName}>");
            logger.LogInformation($"Model state: {(isModelStateValid ? "" : "in")}valid");
            this.stopwatch.Restart();
        }

        private void LogLeavingMessage(string httpMethod, string controllerName, string actionName, bool isModelStateValid)
        {
            this.stopwatch.Stop();
            var time = stopwatch.Elapsed.TotalSeconds;

            this.log.Information($"Executed <{httpMethod}><{controllerName}>.<{actionName}> in <{time}>");
            logger.LogInformation($"Executed <{httpMethod}><{controllerName}>.<{actionName}> in <{time}>");
        }
    }
}