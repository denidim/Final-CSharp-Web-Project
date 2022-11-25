namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.EntityFrameworkCore;

    public class CompanyServiceService : ICompanyServiceService
    {
        private readonly IDeletableEntityRepository<Service> companyServiceRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public CompanyServiceService(
            IDeletableEntityRepository<Service> companyServiceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyServiceRepo = companyServiceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
        }

        public async Task CreateAsync(CreateCompanyServiceInputModel input, string userId)
        {
            var service = new Service
            {
                Title = input.Title,

                // TODO: Add Premium Logic
                Description = input.Description,
                CategoryId = input.CategoryId,
            };

            foreach (var item in input.Packages)
            {
                service.Packages.Add(new Package
                {
                    Price = item.Price,
                });
            }

            // TODO add images

            var company = this.companyRepo.All().FirstOrDefault(x => x.AddedByUserId == userId);

            company.Services.Add(service);

            await this.companyRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetGategoriesAsync()
        {
            return await this.categoryRepo.All().ToListAsync();
        }
    }
}
