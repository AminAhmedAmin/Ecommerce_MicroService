using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<ShoppingCart?> GetCartAsync(string userName)
        {
            var data = await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(data)) return null;
            return JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
        {
            var json = JsonSerializer.Serialize(cart);
            await _distributedCache.SetStringAsync(cart.UserName, json);
            return await GetCartAsync(cart.UserName);
        }

        public async Task<bool> DeleteCartAsync(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
            return true;
        }
    }
}
