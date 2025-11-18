using Catalog.Core.Entities;
using Catalog.Core.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByBrandAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateProductAsync(Product Product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product Product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
