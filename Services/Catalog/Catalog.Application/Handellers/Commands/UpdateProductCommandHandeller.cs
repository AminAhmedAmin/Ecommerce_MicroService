using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handellers.Commands
{
    public class UpdateProductCommandHandeller : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);

            var result = await productRepository.UpdateProductAsync(product);

            if (!result)
            {
                return false;
            }

            var productDto = mapper.Map<ProductResponseDto>(product);

            return true;
        }
    }
}

