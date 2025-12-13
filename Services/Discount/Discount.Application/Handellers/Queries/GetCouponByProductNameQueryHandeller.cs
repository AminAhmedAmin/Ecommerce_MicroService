using AutoMapper;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Queries
{
    public class GetCouponByProductNameQueryHandeller : IRequestHandler<GetCouponByProductNameQuery, CouponResponseDto>
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public GetCouponByProductNameQueryHandeller(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CouponResponseDto> Handle(GetCouponByProductNameQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _repository.GetCouponByProductNameAsync(request.ProductName);
            if (coupon == null) return null!;
            return _mapper.Map<CouponResponseDto>(coupon);
        }
    }
}

