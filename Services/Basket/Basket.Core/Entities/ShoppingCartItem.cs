using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Entities
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string FileImage { get; set; }


    }
}
