using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            
            if (orderToUpdate == null)
            {
                throw new System.Exception($"Order with Id {request.Id} not found");
            }

            _mapper.Map(request, orderToUpdate);
            await _orderRepository.UpdateAsync(orderToUpdate);
            
            return _mapper.Map<OrderResponse>(orderToUpdate);
        }
    }
}
