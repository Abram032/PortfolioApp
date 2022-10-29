using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PortfolioApp.ApplicationCore.AzureFunctions;
using PortfolioApp.ApplicationCore.AzureFunctions.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.ApplicationCore.Features.Images
{
    public sealed class GetAllImages : ApiFunction
    {
        public GetAllImages(ILogger<GetAllImages> logger) : base(logger) { }

        [Function(nameof(GetAllImages))]
        public async Task<List<GetAllImagesResponse>> Run([HttpTrigger(AuthorizationLevel.Function, Http.Get, Route = "v1/images")] HttpRequestData request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            return new List<GetAllImagesResponse>
            {
                new GetAllImagesResponse
                {
                    Guid = Guid.NewGuid(),
                    Title = "Example"
                }
            };
        }

        #region Models
        public sealed class GetAllImagesResponse
        {
            public Guid Guid { get; set; }
            public string Title { get; set; }
        
        }
        #endregion Models
    }
}
