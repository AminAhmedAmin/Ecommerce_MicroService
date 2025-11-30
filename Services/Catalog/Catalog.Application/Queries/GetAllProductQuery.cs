using Catalog.Application.Responses;
using Catalog.Core.Spac;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllProductQuery : IRequest<Pagination<ProductResponseDto>>
    {
        public GetAllProductQuery( CatalogSpecParms parms)
        {
            Parms = parms;
        }

        public CatalogSpecParms Parms { get; }
    }
}
