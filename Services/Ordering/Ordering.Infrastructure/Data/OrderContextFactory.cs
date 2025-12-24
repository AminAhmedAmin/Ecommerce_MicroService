using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            // Get the path to the API project (startup project)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Ordering.API");
            
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            var connectionString = configuration.GetConnectionString("OrderingConnectionString");
            
            optionsBuilder.UseSqlServer(connectionString);

            return new OrderContext(optionsBuilder.Options);
        }
    }
}

