using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart( string Username) 
        {
            UserName = Username;
        }
        public string UserName { get; set; }
        
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
