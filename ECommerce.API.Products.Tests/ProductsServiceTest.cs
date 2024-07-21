using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Profiles;
using ECommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(ProductsServiceTest))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, mapper);

            var products = await productsProvider.GetProductsAsync(default);

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(ProductsServiceTest))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, mapper);

            var product = await productsProvider.GetProductAsync(1, default);

            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task GetProductReturnsNullUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(ProductsServiceTest))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, mapper);

            var product = await productsProvider.GetProductAsync(-1, default);

            Assert.Null(product);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            for (int i = 10; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                { 
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }

            dbContext.SaveChanges();
        }
    }
}