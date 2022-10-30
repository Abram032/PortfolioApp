using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PortfolioApp.ApplicationCore.Features.Middleware;

namespace PortfolioApp.ApplicationCore.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration) => services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingMiddleware<,>));
    }

}