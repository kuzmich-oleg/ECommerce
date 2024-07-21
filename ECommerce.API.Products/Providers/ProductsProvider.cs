using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products.ToListAsync(cancellationToken);
            var models = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);

            return models;
        }

        public async Task<Models.Product?> GetProductAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FindAsync(id, cancellationToken);

            if (product == null) return null;

            var model = _mapper.Map<Db.Product, Models.Product>(product);

            return model;
        }
    }
}
