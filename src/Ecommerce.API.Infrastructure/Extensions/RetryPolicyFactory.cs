using Polly;
using Polly.Extensions.Http;

namespace Ecommerce.API.Infrastructure.Extensions;

public static class RetryPolicyFactory
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => (int)response.StatusCode == 429) // Retry quando status code for 429 (Too Many Requests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}