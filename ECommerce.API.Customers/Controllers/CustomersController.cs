using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider _customerProvider;

        public CustomersController(ICustomersProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll(CancellationToken cancellationToken)
        {
            var customers = await _customerProvider.GetCustomersAsync(cancellationToken);

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var customer = await _customerProvider.GetCustomerAsync(id, cancellationToken);

            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}
