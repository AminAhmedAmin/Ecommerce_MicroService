using System.Collections.Generic;

namespace Basket.Application.Responses
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; }
        public IList<ShoppingCartItemResponse> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
