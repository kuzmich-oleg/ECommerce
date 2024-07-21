using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IProductsService
    {
        Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken);
    }
}
