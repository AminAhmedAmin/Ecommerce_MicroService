using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByProductNameQuery : IRequest<CouponResponseDto>
    {
        public string ProductName { get; set; }

        public GetCouponByProductNameQuery(string productName)
        {
            ProductName = productName;
        }
    }
}

