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
    public class GetAllBrandQueryHandeller : IRequestHandler<GetAllBrandQuery, IList<BrandResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly IBrandRepository brandRepository;

        public GetAllBrandQueryHandeller(IMapper mapper, IBrandRepository brandRepository)
        {
            this.mapper = mapper;
            this.brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponseDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {

            var listbrand =await brandRepository.GetBrandsAsync();

            var listbrandDto = mapper.Map<IList<BrandResponseDto>>(listbrand);

            return listbrandDto;
        }
    }
}
