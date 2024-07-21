using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Products.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider provider;

        public ProductsController(IProductsProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAll(CancellationToken cancellationToken)
        { 
            var products = await provider.GetProductsAsync(cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var product = await provider.GetProductAsync(id, cancellationToken);

            if (product == null) return NotFound();

            return Ok(product);
        }
    }
}
