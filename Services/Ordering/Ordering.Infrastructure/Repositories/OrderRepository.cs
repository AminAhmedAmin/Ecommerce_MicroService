using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        // Add any Order-specific repository methods implementation here
    }
}
