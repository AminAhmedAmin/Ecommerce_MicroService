using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using AutoMapper;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public GetBasketQueryHandler(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetCartAsync(request.UserName);
            if (cart == null) return null!;

            var dto = _mapper.Map<ShoppingCartResponse>(cart);
            return dto;
        }
    }
}
