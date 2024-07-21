using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
