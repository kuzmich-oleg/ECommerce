
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Providers;
using ECommerce.API.Customers.Middleware;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<CustomersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Customers");
            });


            builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
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
