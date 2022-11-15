using System.Net.Http.Headers;

namespace InnoGotchi.ApiClient
{
    public class HttpContextMiddleware : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
