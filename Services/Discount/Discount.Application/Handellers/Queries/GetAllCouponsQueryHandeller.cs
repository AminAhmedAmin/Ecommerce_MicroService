using AutoMapper;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Queries
{
    public class GetAllCouponsQueryHandeller : IRequestHandler<GetAllCouponsQuery, IEnumerable<CouponResponseDto>>
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCouponsQueryHandeller(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CouponResponseDto>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var coupons = await _repository.GetAllCouponsAsync();
            return _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);
        }
    }
}

