using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
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

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var deleted = await _repository.DeleteCartAsync(userName);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
