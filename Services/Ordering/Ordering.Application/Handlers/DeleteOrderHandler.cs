using MediatR;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            
            if (orderToDelete == null)
            {
                return false;
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            return true;
        }
    }
}
