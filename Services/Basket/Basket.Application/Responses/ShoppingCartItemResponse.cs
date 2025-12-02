namespace Basket.Application.Responses
{
    public class ShoppingCartItemResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string FileImage { get; set; }
    }
}
