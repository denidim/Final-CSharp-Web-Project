namespace FindATrade.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Бояджия" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Програмист" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Графичен Дизайн" });

            await dbContext.SaveChangesAsync();
        }
    }
}
