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
    public class GetProductByNameQueryHandeller : IRequestHandler<GetProductByNameQuery, ProductResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductByNameQueryHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<ProductResponseDto> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByNameAsync(request.Name);

            if (product == null)
            {
                return null;
            }

            var productDto = mapper.Map<ProductResponseDto>(product);

            return productDto;
        }
    }
}

