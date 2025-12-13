using AutoMapper;
using Discount.Application.Responses;
using Discount.Core.Entities;

namespace Discount.Application.Mapper
{
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<Coupon, CouponResponseDto>().ReverseMap();
        }
    }
}

