using Bogus;
using Server.Domain.Entities;
using Server.Infrastructure.Context;

namespace Server.WebAPI;

public static class Helper
{
    public static async Task GenerateData(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            CancellationToken cancellationToken = new();

            if (!context.Set<Product>().Any())
            {
                Faker faker = new();
                List<Product> products = new();

                for (int i = 0; i < 100000; i++)
                {
                    products.Add(new Product()
                    {
                        Name = faker.Commerce.ProductName(),
                        Price = decimal.Parse(faker.Commerce.Price(100, 1000, 2)),
                        Stock = faker.Random.Int(1, 100)
                    });
                }

                await context
                    .Set<Product>()
                    .AddRangeAsync(products, cancellationToken);

                await context
                    .SaveChangesAsync(cancellationToken);
            }
        }
    }
}
