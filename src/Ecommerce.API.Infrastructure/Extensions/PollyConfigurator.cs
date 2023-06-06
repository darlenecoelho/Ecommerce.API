using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Ecommerce.API.Infrastructure.Extensions
{
    public static class PollyConfigurator
    {
        public static void ConfigurePolly(IServiceCollection services)
        {
            var timeoutInSeconds = 30;
            var eventsBeforeBreaking = 3;
            var durationOfBreakInMinutes = 5;

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(timeoutInSeconds));

            services.AddHttpClient("EcommerceClient")
                .AddPolicyHandler(timeoutPolicy)
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: eventsBeforeBreaking,
                        durationOfBreak: TimeSpan.FromMinutes(durationOfBreakInMinutes)));
        }
    }
}
