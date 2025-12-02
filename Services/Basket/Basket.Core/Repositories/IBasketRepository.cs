using Basket.Core.Entities;
using System.Threading.Tasks;

namespace Basket.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetCartAsync(string userName);

        Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart);

        Task<bool> DeleteCartAsync(string userName);
    }
}
