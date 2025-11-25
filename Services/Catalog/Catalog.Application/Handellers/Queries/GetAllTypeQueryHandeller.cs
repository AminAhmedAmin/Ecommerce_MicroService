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
    public class GetAllTypeQueryHandeller : IRequestHandler<GetAllTypeQuery, IList<TypeResponseDto>>

    {
        private readonly ITypeRepository typeRepository;
        private readonly IMapper mapper;

        public GetAllTypeQueryHandeller( ITypeRepository  typeRepository, IMapper mapper)
        {
            this.typeRepository = typeRepository;
            this.mapper = mapper;
        }

        public async Task<IList<TypeResponseDto>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {

            var listtype = await typeRepository.GetTypesAsync();

            var listtypeDto = mapper.Map<IList<TypeResponseDto>>(listtype);

            return listtypeDto;
        }
    }
}
