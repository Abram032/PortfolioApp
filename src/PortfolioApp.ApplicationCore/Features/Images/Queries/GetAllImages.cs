using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PortfolioApp.ApplicationCore.AzureFunctions;
using PortfolioApp.ApplicationCore.AzureFunctions.Abstractions;
using PortfolioApp.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static PortfolioApp.ApplicationCore.Features.Images.Queries.GetAllImages.GetAllImagesResponse;

namespace PortfolioApp.ApplicationCore.Features.Images.Queries
{
    public sealed class GetAllImages : ApiFunction
    {
        public GetAllImages(ILogger<GetAllImages> logger, ISender sender) : base(logger, sender) { }

        [Function(nameof(GetAllImages))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, Http.Get, Route = "v1/images")] HttpRequestData request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _sender.Send(new GetAllImagesQuery());

            return await result.Match(
                async value => 
                { 
                    var response = request.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(value);
                    return response;
                }, 
                async () =>
                {
                    return request.CreateResponse(HttpStatusCode.NotFound);
                });
        }

        #region Handler
        public sealed class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, Maybe<GetAllImagesResponse>>
        {
            private readonly ILogger<GetAllImagesQueryHandler> _logger;

            public GetAllImagesQueryHandler(ILogger<GetAllImagesQueryHandler> logger)
            {
                _logger = logger;
            }

            public async Task<Maybe<GetAllImagesResponse>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
            {
                var response = new GetAllImagesResponse
                {
                    Images = Enumerable.Range(0, 3).Select(x => new GetAllImagesResponseImage
                    {
                        Guid = Guid.NewGuid(),
                        Title = $"Example {x}"
                    })
                };

                return response;
            }
        }
        #endregion Handler

        #region Models
        public sealed class GetAllImagesQuery : IRequest<Maybe<GetAllImagesResponse>> { }
        public sealed class GetAllImagesResponse
        {
            public IEnumerable<GetAllImagesResponseImage> Images { get; set; } = Enumerable.Empty<GetAllImagesResponseImage>();

            public sealed class GetAllImagesResponseImage
            {
                public Guid Guid { get; set; }
                public string Title { get; set; }
            }
        }
        #endregion Models
    }
}
