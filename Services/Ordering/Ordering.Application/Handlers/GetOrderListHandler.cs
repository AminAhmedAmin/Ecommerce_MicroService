using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetAsync(o => o.UserName == request.UserName);
            return _mapper.Map<List<OrderResponse>>(orderList);
        }
    }
}
