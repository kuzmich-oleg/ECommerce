namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<IEnumerable<Models.Order>> GetOrdersAsync(int customerId, CancellationToken cancellationToken);
        Task<Models.Order?> GetOrderAsync(int id, CancellationToken cancellationToken);
    }
}
