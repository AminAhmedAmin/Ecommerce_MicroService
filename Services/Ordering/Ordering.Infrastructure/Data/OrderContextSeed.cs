using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Entities;
using System;
using System.Linq;

namespace Ordering.Infrastructure.Data
{
    public static class OrderContextSeed
    {
        public static void Seed(OrderContext context, IServiceProvider serviceProvider)
        {
            if (context.Orders.Any())
                return;

            context.Orders.AddRange(
                new Order
                {
                    UserName = "test_user",
                    TotalPrice = 100,
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@example.com",
                    AddressLine = "123 Main St",
                    Country = "USA",
                    State = "NY",
                    ZipCode = "10001",
                    CardName = "John Doe",
                    CardNumber = "4111111111111111",
                    Expiration = "12/25",
                    CVV = "123",
                    PaymentMethod = 1
                }
            );

            context.SaveChanges();
        }
    }
}
