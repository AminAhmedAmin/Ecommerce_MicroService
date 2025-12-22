using Ordering.Core.Entities;

namespace Ordering.Core.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        // Add any Order-specific repository methods here
    }
}
