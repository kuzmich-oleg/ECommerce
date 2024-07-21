using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _orderProvider;

        public OrdersController(IOrdersProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] int customerId, CancellationToken cancellationToken)
        { 
            var orders = await _orderProvider.GetOrdersAsync(customerId, cancellationToken);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var order = await _orderProvider.GetOrderAsync(id, cancellationToken);

            if (order == null) return NotFound();

            return Ok(order);
        }
    }
}
