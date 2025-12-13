using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Responses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Commands
{
    public class UpdateCouponCommandHandeller : IRequestHandler<UpdateCouponCommand, CouponResponseDto>
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCouponCommandHandeller(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CouponResponseDto> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var existingCoupon = await _repository.GetCouponByIdAsync(request.Id);
            if (existingCoupon == null) return null!;

            existingCoupon.ProductName = request.ProductName;
            existingCoupon.Description = request.Description;
            existingCoupon.Amount = request.Amount;
            existingCoupon.ExpiresAt = request.ExpiresAt;
            existingCoupon.IsActive = request.IsActive;

            var updated = await _repository.UpdateCouponAsync(existingCoupon);
            if (!updated) return null!;

            var updatedCoupon = await _repository.GetCouponByIdAsync(request.Id);
            return _mapper.Map<CouponResponseDto>(updatedCoupon);
        }
    }
}

