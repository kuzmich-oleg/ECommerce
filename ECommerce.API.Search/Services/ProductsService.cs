using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductsService");
                var result = await client.GetAsync("products", cancellationToken);

                if (!result.IsSuccessStatusCode) return [];

                var content = await result.Content.ReadAsByteArrayAsync(cancellationToken);
                var products = JsonSerializer.Deserialize<IList<Product>>(content, _serializerOptions);

                return products ?? [];
            }
            catch
            {
                return [];
            }
        }
    }
}
