using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrdersService ordersService, IProductsService productsService,
            ICustomerService customerService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customerService = customerService;
        }
        public async Task<dynamic?> SearchAsync(int customerId, CancellationToken cancellationToken)
        {
            var orders = await _ordersService.GetOrdersAsync(customerId, cancellationToken);
            var products = await _productsService.GetProductsAsync(cancellationToken);
            var customer = await _customerService.GetCustomerAsync(customerId, cancellationToken);

            if (orders.Count == 0) return new { };

            SetAdditionalInfoForOrder(orders, products, customer);
            return new { Orders = orders };
        }

        private void SetAdditionalInfoForOrder(IList<Order> orders, IList<Product> products, Customer? customer)
        {
            foreach (var order in orders)
            {
                order.Items?.ForEach(item =>
                {
                    item.ProductName = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "Product information is not available";
                });
                order.CustomerName = customer?.Name ?? "Customer information is not available";
            }
        }
    }
}
