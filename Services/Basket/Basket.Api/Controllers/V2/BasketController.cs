using Asp.Versioning;
using Basket.Api.Controllers;
using Basket.Application.Commands;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Basket.Api.Controllers.V2
{
    [ApiVersion("2")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository, IMediator mediator, ILogger<BasketController> logger)
        {
            _repository = repository;
            _mediator = mediator;
            _logger = logger;
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
            _logger.LogInformation("V2 UpdateBasket called for user {UserName}", cart.UserName);
            var updated = await _repository.UpdateCartAsync(cart);
            return Ok(updated);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            _logger.LogInformation("V2 Checkout called for user {UserName}", basketCheckout.UserName);
            // Use V2 Command
            var command = new CheckoutBasketCommandV2(basketCheckout);
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
