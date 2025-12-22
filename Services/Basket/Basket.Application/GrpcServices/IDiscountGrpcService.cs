using System.Threading.Tasks;

namespace Basket.Application.GrpcServices
{
    public interface IDiscountGrpcService
    {
        Task<CouponModel> GetDiscount(string productName);
    }
}
