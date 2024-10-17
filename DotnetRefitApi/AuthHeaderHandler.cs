using System.Net.Http.Headers;

namespace DotnetRefitApi;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IConfiguration _config;
    public AuthHeaderHandler(IConfiguration config)
    {
        _config = config;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = _config
            .GetValue<string>("ExternalApiToken");

        request.Headers.Authorization = 
            new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}
