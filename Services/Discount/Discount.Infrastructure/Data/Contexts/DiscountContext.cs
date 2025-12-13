using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Discount.Infrastructure.Data.Contexts
{
    public class DiscountContext : IDiscountContext
    {
        private readonly string _connectionString;

        public DiscountContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DiscountDb") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DiscountDb' not found.");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
