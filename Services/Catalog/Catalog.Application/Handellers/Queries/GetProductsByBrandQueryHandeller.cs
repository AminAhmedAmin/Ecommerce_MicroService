using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handellers.Queries
{
    public class GetProductsByBrandQueryHandeller : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductsByBrandQueryHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<IList<ProductResponseDto>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsByBrandAsync(request.BrandName);

            var productsDto = mapper.Map<IList<ProductResponseDto>>(products);

            return productsDto;
        }
    }
}

