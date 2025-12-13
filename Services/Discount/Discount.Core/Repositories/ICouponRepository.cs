using Discount.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Core.Repositories
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAllCouponsAsync();
        Task<Coupon> GetCouponByIdAsync(int id);
        Task<Coupon> GetCouponByProductNameAsync(string productName);
        Task<Coupon> CreateCouponAsync(Coupon coupon);
        Task<bool> UpdateCouponAsync(Coupon coupon);
        Task<bool> DeleteCouponAsync(int id);
    }
}

