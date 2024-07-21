using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer { Id = 1, Name = "John", Address = "Lviv" },
                    new Customer { Id = 2, Name = "Maria", Address = "Kuiv" }
                );
        }
    }
}
