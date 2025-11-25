using Catalog.Application.Commands;
using Catalog.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mapper
{
    public class ProductMapppingProfile : AutoMapper.Profile
    {
        public ProductMapppingProfile()
        {
            // Create your mapping configurations here
            // Example:
            // CreateMap<SourceType, DestinationType>();

            CreateMap<ProductBrand, Application.Responses.BrandResponseDto>().ReverseMap();
            CreateMap<Product, Application.Responses.ProductResponseDto>().ReverseMap();
            CreateMap<ProductType, Application.Responses.TypeResponseDto>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

        }
    }
}
