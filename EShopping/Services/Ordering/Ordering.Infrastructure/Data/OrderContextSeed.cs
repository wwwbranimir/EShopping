using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new () {
                    UserName = "bane",
                    FirstName = "Branimir",
                    LastName = "Bzenic",
                    EmailAddress = "branimir.bzenic@gmail.com",
                    AddressLine = "Kneza Mihaila 1",
                    Country = "Serbia",
                    State = "Beogradski pasaluk",
                    ZipCode = "21000",
                    CardName = "Branimir Bzenic",
                    CardNumber = "1234567890123456",
                    Expiration = "12/23",
                    Cvv = "123",
                    PaymentMethod = 1,
                    TotalPrice = 10000,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = "bane"
                    }
            };
        }
    }
}
