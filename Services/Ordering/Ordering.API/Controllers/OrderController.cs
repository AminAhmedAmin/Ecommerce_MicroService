using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}", Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponse>> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
