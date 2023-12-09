using MediatR;

namespace Ripley.Api.Provider.CrossCutting.Configuration.Services
{
    public class HttpClientService
    {
        private IHttpClientFactory _factory;
        public HttpClientService(IMediator mediator, IHttpClientFactory httpClientFactory)
        {
            _factory = httpClientFactory;
        }
    }
}
