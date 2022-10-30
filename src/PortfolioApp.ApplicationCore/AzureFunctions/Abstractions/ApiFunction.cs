using MediatR;
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
        protected readonly ISender _sender;
        
        public ApiFunction(ILogger<ApiFunction> logger, ISender sender)
        {
            _logger =  logger;
            _sender = sender;
        }
    }
}
