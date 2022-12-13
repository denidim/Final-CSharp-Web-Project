namespace FindATrade.Services.Data
{
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepo;
        private readonly IDeletableEntityRepository<Company> companiesRepo;
        private readonly IDeletableEntityRepository<Service> servicesRepo;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepo,
            IDeletableEntityRepository<Company> companiesRepo,
            IDeletableEntityRepository<Service> servicesRepo)
        {
            this.categoriesRepo = categoriesRepo;
            this.companiesRepo = companiesRepo;
            this.servicesRepo = servicesRepo;
        }

        public IndexOutputViewModel GetCounts()
        {
            var data = new IndexOutputViewModel
            {
                CategoriesCount = this.categoriesRepo.All().Count(),
                CompaniesCount = this.companiesRepo.All().Count(),
                ServicesCount = this.servicesRepo.All().Count(),
            };

            return data;
        }
    }
}
