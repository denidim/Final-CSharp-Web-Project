namespace FindATrade.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PaidOrderSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PaidOrders.Any())
            {
                return;
            }

            await dbContext.PaidOrderPackageTypes.AddAsync(new Models.PaidOrderPackageType { Name = "Regular Subscription", Terms = "Your service will be shown on the first pages for 30 days", Price = 10.00m });

            await dbContext.SaveChangesAsync();
        }
    }
}
