using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 },
                    new Product { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 },
                    new Product { Id = 3, Name = "Monitor", Price = 150, Inventory = 300 },
                    new Product { Id = 4, Name = "CPU", Price = 200, Inventory = 150 }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
