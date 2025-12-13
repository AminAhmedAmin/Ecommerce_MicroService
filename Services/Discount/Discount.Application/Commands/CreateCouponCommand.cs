using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateCouponCommand : IRequest<CouponResponseDto>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }

        public CreateCouponCommand(
            string productName,
            string description,
            decimal amount,
            DateTime? expiresAt = null,
            bool isActive = true)
        {
            ProductName = productName;
            Description = description;
            Amount = amount;
            ExpiresAt = expiresAt;
            IsActive = isActive;
        }
    }
}

