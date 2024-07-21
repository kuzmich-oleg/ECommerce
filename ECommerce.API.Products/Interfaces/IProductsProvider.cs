using ECommerce.API.Products.Models;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken);
    }
}
