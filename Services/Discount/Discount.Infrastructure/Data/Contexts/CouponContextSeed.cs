using Dapper;
using Discount.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Discount.Infrastructure.Data.Contexts
{
    public static class CouponContextSeed
    {
        public static async Task SeedDataAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DiscountDb");
            if (string.IsNullOrEmpty(connectionString))
                return;

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            // Create table if it doesn't exist
            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Coupons (
                    Id SERIAL PRIMARY KEY,
                    ProductName VARCHAR(255) NOT NULL,
                    Description TEXT,
                    Amount DECIMAL(18,2) NOT NULL,
                    CreatedAt TIMESTAMP NOT NULL,
                    ExpiresAt TIMESTAMP,
                    IsActive BOOLEAN NOT NULL DEFAULT true
                );";

            await connection.ExecuteAsync(createTableQuery);

            // Check if data exists
            var countQuery = "SELECT COUNT(*) FROM Coupons";
            var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery);

            if (count == 0)
            {
                // Insert seed data
                var insertQuery = @"
                    INSERT INTO Coupons (ProductName, Description, Amount, CreatedAt, ExpiresAt, IsActive)
                    VALUES 
                        (@ProductName1, @Description1, @Amount1, @CreatedAt, @ExpiresAt1, @IsActive),
                        (@ProductName2, @Description2, @Amount2, @CreatedAt, @ExpiresAt2, @IsActive);";

                await connection.ExecuteAsync(insertQuery, new
                {
                    ProductName1 = "IPhone X",
                    Description1 = "Discount for IPhone X",
                    Amount1 = 150,
                    ProductName2 = "Samsung 10",
                    Description2 = "Discount for Samsung 10",
                    Amount2 = 100,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt1 = DateTime.UtcNow.AddDays(30),
                    ExpiresAt2 = DateTime.UtcNow.AddDays(30),
                    IsActive = true
                });
            }
        }
    }
}
