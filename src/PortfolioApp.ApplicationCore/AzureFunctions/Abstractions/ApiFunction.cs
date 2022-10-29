using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.ApplicationCore.AzureFunctions.Abstractions
{
    public abstract class ApiFunction
    {
        protected readonly ILogger<ApiFunction> _logger;
        
        public ApiFunction(ILogger<ApiFunction> logger)
        {
            _logger =  logger;
        }
    }
}
