using Catalog.Core.Entities;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Contexts
{
    public static class ProductContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            // 1) Check if collection already has data
            var hasproducts = await productCollection.Find(_ => true).AnyAsync();
            if (hasproducts)
                return;

            // 2) Build file path
            var filePath = Path.Combine("Data", "SeedData", "products.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not exists: {filePath}");
                return;
            }

            // 3) Read JSON file
            var brandData = await File.ReadAllTextAsync(filePath);

            // 4) Deserialize
            var Products = JsonSerializer.Deserialize<List<Product>>(brandData);

            if (Products == null || Products.Count == 0)
                return;

            // 5) Insert into MongoDB
            await productCollection.InsertManyAsync(Products);

            Console.WriteLine("Products seed completed.");
        }
    }
}
