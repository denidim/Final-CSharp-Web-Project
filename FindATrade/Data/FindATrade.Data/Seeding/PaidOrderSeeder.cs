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

            await dbContext.PaidOrders.AddAsync(new Models.PaidOrder { Name = "Regular", Price = 10.00m, Terms = "Your Service will be on the front page and first on serches", StartDate = default(DateTime), EndDate = default(DateTime) });

            await dbContext.SaveChangesAsync();
        }
    }
}
