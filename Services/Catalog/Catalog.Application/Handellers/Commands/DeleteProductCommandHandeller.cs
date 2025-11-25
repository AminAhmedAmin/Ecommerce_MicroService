using Catalog.Application.Commands;
using Catalog.Core.Repositories;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handellers.Commands
{
    public class DeleteProductCommandHandeller : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandeller(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await productRepository.DeleteProductAsync(request.Id);

            return result;
        }
    }
}

