using System.Data;

namespace Discount.Infrastructure.Data.Contexts
{
    public interface IDiscountContext
    {
        IDbConnection CreateConnection();
    }
}
