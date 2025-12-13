using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handellers.Commands
{
    public class DeleteCouponCommandHandeller : IRequestHandler<DeleteCouponCommand, bool>
    {
        private readonly ICouponRepository _repository;

        public DeleteCouponCommandHandeller(ICouponRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteCouponAsync(request.Id);
        }
    }
}

