using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PortfolioApp.ApplicationCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

await new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(worker =>
    {
        worker.UseDefaultWorkerMiddleware();
    })
    .ConfigureLogging(logging =>
    {
        //logging.AddConsole();
    })
    .ConfigureServices((host, services) =>
    {
        services.AddApplicationCore(host.Configuration);
    })
    .Build()
    .RunAsync();