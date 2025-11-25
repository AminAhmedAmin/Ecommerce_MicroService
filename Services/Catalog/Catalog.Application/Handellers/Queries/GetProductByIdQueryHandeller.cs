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
    public class GetProductByIdQueryHandeller : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductByIdQueryHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                return null;
            }

            var productDto = mapper.Map<ProductResponseDto>(product);

            return productDto;
        }
    }
}

