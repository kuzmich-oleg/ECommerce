using AutoMapper;
using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext ordersDbContext, IMapper mapper)
        {
            _ordersDbContext = ordersDbContext;
            _ordersDbContext.Database.EnsureCreated();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Order>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
        {
            var orders = await _ordersDbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Items)
                .ToListAsync(cancellationToken);
            var models = _mapper.Map<IEnumerable<Order>, IEnumerable<Models.Order>>(orders);

            return models;
        }

        public async Task<Models.Order?> GetOrderAsync(int id, CancellationToken cancellationToken)
        {
            var order = await _ordersDbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null) return null;

            var model = _mapper.Map<Order, Models.Order>(order);
            return model;
        }
    }
}
