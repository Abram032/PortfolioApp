using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.ApplicationCore.Features.Middleware
{
    internal sealed class LoggingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingMiddleware<TRequest, TResponse>> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation($"Processing request {name}");

            var result = await next();

            _logger.LogInformation($"Processed request {name}");

            return result;
        }
    }
}