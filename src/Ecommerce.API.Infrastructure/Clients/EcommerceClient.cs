namespace Ecommerce.API.Infrastructure.Clients
{
    public class EcommerceClient
    {
        public HttpClient HttpClient { get; }

        public EcommerceClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
