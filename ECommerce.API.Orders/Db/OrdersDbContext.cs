using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasData(
                    new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total = 55 },
                    new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, Total = 350 }
                );

            modelBuilder.Entity<OrderItem>()
                .HasData(
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 20 },
                    new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 5 },
                    new OrderItem { Id = 3, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 150 },
                    new OrderItem { Id = 4, OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 200 }
                );
        }
    }
}
