using Discount.Grpc.Protos;
using Basket.Application.GrpcServices;
using System.Threading.Tasks;

namespace Basket.Infrastructure.GrpcServices
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<Basket.Application.GrpcServices.CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountByProductNameRequest { ProductName = productName };
            
            try
            {
                var couponModel = await _discountProtoService.GetDiscountByProductNameAsync(discountRequest);
                return new Basket.Application.GrpcServices.CouponModel
                {
                    Id = couponModel.Id,
                    ProductName = couponModel.ProductName,
                    Description = couponModel.Description,
                    Amount = (int)couponModel.Amount
                };
            }
            catch (System.Exception)
            {
                // In case of error (e.g. service down), return no discount?
                // Or throw?
                // For now, let's just log or rethrow. 
                // Since I don't have a logger here yet, I'll just return a default/empty coupon or rethrow.
                // Rethrowing is safer to debug.
                throw;
            }
        }
    }
}
