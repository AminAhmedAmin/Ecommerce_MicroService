using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Contexts;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private readonly ICatalgContext _context;

        public ProductRepository(ICatalgContext context)
        {
            _context = context;
        }

        // =================== Products ===================

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();


        }


        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products
                .Find(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetProductByBrandAsync(string brandName)
        {
            return await _context.Products
                .Find(p => p.Brand.Name == brandName)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brandName)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Brand.Name, new MongoDB.Bson.BsonRegularExpression(brandName, "i"));
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var result = await _context.Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await _context.Products
                .DeleteOneAsync(p => p.Id == id);

            return result.DeletedCount > 0 && result.IsAcknowledged;
        }

        // =================== Brands ===================

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return await _context.Brands
                .Find(b => true)
                .ToListAsync();
        }

        // =================== Types ===================

        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            return await _context.Types
                .Find(t => true)
                .ToListAsync();
        }
    }
}

