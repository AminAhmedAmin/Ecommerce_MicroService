using Basket.Application.Commands;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _repository;
        private readonly IMediator _mediator;

        public BasketController(IBasketRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var cart = await _repository.GetCartAsync(userName);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            var updated = await _repository.UpdateCartAsync(cart);
            return Ok(updated);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var command = new CheckoutBasketCommand(basketCheckout);
            var result = await _mediator.Send(command);
            if (!result) return BadRequest("Basket not found");
            return Accepted();
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var deleted = await _repository.DeleteCartAsync(userName);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }
    }
}
