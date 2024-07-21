using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<IList<Order>> GetOrdersAsync(int customerId, CancellationToken cancellationToken);
    }
}
