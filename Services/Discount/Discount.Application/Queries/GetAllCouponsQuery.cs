using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetAllCouponsQuery : IRequest<IEnumerable<CouponResponseDto>>
    {
    }
}

