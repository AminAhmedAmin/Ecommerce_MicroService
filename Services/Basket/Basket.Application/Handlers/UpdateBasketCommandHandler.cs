using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBasketCommandHandler(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var updated = await _repository.UpdateCartAsync(request.Cart);
            var dto = _mapper.Map<ShoppingCartResponse>(updated);
            return dto;
        }
    }
}
