using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetCouponByIdQuery(request.Id);
            var coupon = await _mediator.Send(query);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ID {request.Id} not found."));
            }

            return MapToCouponModel(coupon);
        }

        public override async Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsRequest request, ServerCallContext context)
        {
            var query = new GetAllCouponsQuery();
            var coupons = await _mediator.Send(query);

            var response = new GetAllDiscountsResponse();
            response.Coupons.AddRange(coupons.Select(MapToCouponModel));

            return response;
        }

        public override async Task<CouponModel> GetDiscountByProductName(GetDiscountByProductNameRequest request, ServerCallContext context)
        {
            var query = new GetCouponByProductNameQuery(request.ProductName);
            var coupon = await _mediator.Send(query);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product '{request.ProductName}' not found."));
            }

            return MapToCouponModel(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateCouponCommand(
                request.ProductName,
                request.Description,
                (decimal)request.Amount,
                string.IsNullOrEmpty(request.ExpiresAt) ? null : DateTime.Parse(request.ExpiresAt),
                request.IsActive
            );

            var coupon = await _mediator.Send(command);
            return MapToCouponModel(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateCouponCommand(
                request.Id,
                request.ProductName,
                request.Description,
                (decimal)request.Amount,
                string.IsNullOrEmpty(request.ExpiresAt) ? null : DateTime.Parse(request.ExpiresAt),
                request.IsActive
            );

            var coupon = await _mediator.Send(command);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ID {request.Id} not found."));
            }

            return MapToCouponModel(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteCouponCommand(request.Id);
            var success = await _mediator.Send(command);

            return new DeleteDiscountResponse { Success = success };
        }

        private static CouponModel MapToCouponModel(Discount.Application.Responses.CouponResponseDto coupon)
        {
            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = (double)coupon.Amount,
                CreatedAt = coupon.CreatedAt.ToString("O"),
                ExpiresAt = coupon.ExpiresAt?.ToString("O") ?? string.Empty,
                IsActive = coupon.IsActive
            };
        }
    }
}

