using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Customer>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _dbContext.Customers.ToListAsync(cancellationToken);
            var models = _mapper.Map<IEnumerable<Customer>, IEnumerable<Models.Customer>>(customers);

            return models;
        }

        public async Task<Models.Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FindAsync(id, cancellationToken);

            if (customer == null) return null;

            var model = _mapper.Map<Customer, Models.Customer>(customer);
            return model;
        }
    }
}
