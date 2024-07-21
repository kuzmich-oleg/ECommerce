namespace ECommerce.API.Search.Interfaces
{
    public interface ISearchService
    {
        Task<dynamic?> SearchAsync(int customerId, CancellationToken cancellationToken);
    }
}
