namespace Discount.Application.Responses
{
    public class CouponResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
    }
}

