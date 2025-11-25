using Catalog.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
         Task<IEnumerable<Entities.Product>> GetProductsAsync();

         Task<Entities.Product> GetProductByIdAsync(string id);

        Task<Entities.Product> GetProductByNameAsync(string name);

        Task<IEnumerable<Entities.Product>> GetProductsByNameAsync(string name);

        Task<Entities.Product> GetProductByBrandAsync(string name);

        Task<IEnumerable<Entities.Product>> GetProductsByBrandAsync(string brandName);

        Task<bool>CreateProductAsync(Product Product);
        Task<bool> UpdateProductAsync(Product Product);

        Task<bool> DeleteProductAsync(string id);

    }
}
