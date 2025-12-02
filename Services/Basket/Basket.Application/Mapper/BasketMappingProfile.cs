using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mapper
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
        }
    }
}
