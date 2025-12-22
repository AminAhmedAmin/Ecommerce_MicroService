using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CheckoutOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);
            return _mapper.Map<OrderResponse>(newOrder);
        }
    }
}
