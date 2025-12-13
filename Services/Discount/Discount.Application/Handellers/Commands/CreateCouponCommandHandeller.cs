using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Commands
{
    public class CreateCouponCommandHandeller : IRequestHandler<CreateCouponCommand, CouponResponseDto>
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public CreateCouponCommandHandeller(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CouponResponseDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = new Coupon
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                ExpiresAt = request.ExpiresAt,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            var createdCoupon = await _repository.CreateCouponAsync(coupon);
            return _mapper.Map<CouponResponseDto>(createdCoupon);
        }
    }
}

