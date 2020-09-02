using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace StarWars.Web
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandlerFilter> _logger;

        public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger)
        {
            _logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var path = $"StarWarsService/{context.HttpContext.Request.Path.Value}";

            _logger.LogError($"Unexpected exception ocurred while attempting to reach {path}.{Environment.NewLine}{context.Exception}");

            context.ExceptionHandled = false;

            await base.OnExceptionAsync(context).ConfigureAwait(false);
        }
    }
}
