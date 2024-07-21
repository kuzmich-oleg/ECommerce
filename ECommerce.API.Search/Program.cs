
using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Middleware;
using ECommerce.API.Search.Services;
using Polly;

namespace ECommerce.API.Search
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ISearchService, SearchService>();

            builder.Services.AddHttpClient("OrdersService", config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["Services:Orders"] ?? string.Empty);
            });
            builder.Services.AddHttpClient("CustomersService", config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["Services:Customers"] ?? string.Empty);
            });
            builder.Services
                .AddHttpClient("ProductsService", config =>
                {
                    config.BaseAddress = new Uri(builder.Configuration["Services:Products"] ?? string.Empty);
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

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
