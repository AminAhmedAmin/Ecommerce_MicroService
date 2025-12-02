using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IBasketRepository _repository;

        public DeleteBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteCartAsync(request.UserName);
        }
    }
}
