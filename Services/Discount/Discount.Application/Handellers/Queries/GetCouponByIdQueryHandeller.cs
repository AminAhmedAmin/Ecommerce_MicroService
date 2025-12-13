using AutoMapper;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Queries
{
    public class GetCouponByIdQueryHandeller : IRequestHandler<GetCouponByIdQuery, CouponResponseDto>
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public GetCouponByIdQueryHandeller(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CouponResponseDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _repository.GetCouponByIdAsync(request.Id);
            if (coupon == null) return null!;
            return _mapper.Map<CouponResponseDto>(coupon);
        }
    }
}

