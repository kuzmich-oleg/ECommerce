
using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Middleware;
using ECommerce.API.Orders.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IOrdersProvider, OrdersProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Orders");
            });
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
