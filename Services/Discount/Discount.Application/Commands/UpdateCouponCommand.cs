using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateCouponCommand : IRequest<CouponResponseDto>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }

        public UpdateCouponCommand(
            int id,
            string productName,
            string description,
            decimal amount,
            DateTime? expiresAt = null,
            bool isActive = true)
        {
            Id = id;
            ProductName = productName;
            Description = description;
            Amount = amount;
            ExpiresAt = expiresAt;
            IsActive = isActive;
        }
    }
}

