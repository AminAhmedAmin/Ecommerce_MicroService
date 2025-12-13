using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByIdQuery : IRequest<CouponResponseDto>
    {
        public int Id { get; set; }

        public GetCouponByIdQuery(int id)
        {
            Id = id;
        }
    }
}

