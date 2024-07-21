using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions = new() 
        {
            PropertyNameCaseInsensitive = true
        };

        public OrdersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Order>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"orders?customerId={customerId}", cancellationToken);

                if (!response.IsSuccessStatusCode) return [];

                var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                var orders = JsonSerializer.Deserialize<IList<Order>>(content, _serializerOptions);

                return orders ?? [];
            }
            catch
            {
                return [];
            }
        }
    }
}
