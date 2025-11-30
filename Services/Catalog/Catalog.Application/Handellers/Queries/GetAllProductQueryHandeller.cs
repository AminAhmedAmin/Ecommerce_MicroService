using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Spac;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handellers.Queries
{
    public class GetAllProductQueryHandeller : IRequestHandler<GetAllProductQuery, Pagination<ProductResponseDto>>
    {


        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetAllProductQueryHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var listproduct = await productRepository.GetProductsAsync(request.Parms);

            var listproductDto = mapper.Map<Pagination<ProductResponseDto>>(listproduct);

            return listproductDto;
        }
    }
}
