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


            await dbContext.SaveChangesAsync();
        }
    }
}
