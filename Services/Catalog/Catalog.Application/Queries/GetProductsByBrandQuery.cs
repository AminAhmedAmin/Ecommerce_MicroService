using Catalog.Application.Responses;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductsByBrandQuery : IRequest<IList<ProductResponseDto>>
    {
        public string BrandName { get; set; }

        public GetProductsByBrandQuery(string brandName)
        {
            BrandName = brandName;
        }
    }
}

