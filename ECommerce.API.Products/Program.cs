
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Middleware;
using ECommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products");
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
