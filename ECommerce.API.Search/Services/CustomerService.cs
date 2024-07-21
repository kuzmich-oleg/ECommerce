using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions = new ()
        {
            PropertyNameCaseInsensitive = true
        };

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomersService");
                var result = await client.GetAsync($"customers/{id}", cancellationToken);

                if (!result.IsSuccessStatusCode) return null;

                var content = await result.Content.ReadAsByteArrayAsync(cancellationToken);
                var customer = JsonSerializer.Deserialize<Customer>(content, _serializerOptions);

                return customer;
            }
            catch { return null; }
        }
    }
}
