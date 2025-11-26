using Catalog.Core.Entities;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public  static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
        {
            // 1) Check if collection already has data
            var hasBrands = await brandCollection.Find(_ => true).AnyAsync();
            if (hasBrands)
                return;

            // 2) Build file path
            var filePath = Path.Combine("Data", "SeedData", "brands.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not exists: {filePath}");
                return;
            }

            // 3) Read JSON file
            var brandData = await File.ReadAllTextAsync(filePath);

            // 4) Deserialize
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands == null || brands.Count == 0)
                return;

            // 5) Insert into MongoDB
            await brandCollection.InsertManyAsync(brands);

            Console.WriteLine("Brand seed completed.");
        }

    }
}
