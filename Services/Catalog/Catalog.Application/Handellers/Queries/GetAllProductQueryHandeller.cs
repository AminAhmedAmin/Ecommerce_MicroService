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
    public class GetAllProductQueryHandeller : IRequestHandler<GetAllProductQuery, IList<ProductResponseDto>>
    {


        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetAllProductQueryHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var listproduct = await productRepository.GetProductsAsync();

            var listproductDto = mapper.Map<IList<ProductResponseDto>>(listproduct);

            return listproductDto;
        }
    }
}
