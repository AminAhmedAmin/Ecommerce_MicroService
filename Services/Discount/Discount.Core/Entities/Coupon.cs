namespace Discount.Core.Entities
{
    public class Coupon : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
    }
}

