using ECommerce.API.Customers.Models;

namespace ECommerce.API.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
