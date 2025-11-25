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
    public class CreateProductCommandHandeller : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandeller(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Summary = request.Summary,
                Price = request.Price,
                ImageFile = request.ImageFile,
                Brand = request.Brand,
                Type = request.Type
            };
         //   var product = mapper.Map<product>(request);

            var result = await productRepository.CreateProductAsync(product);

            if (!result)
            {
                return null;
            }

            var productDto = mapper.Map<ProductResponseDto>(product);

            return productDto;
        }
    }
}

