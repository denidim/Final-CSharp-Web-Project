namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.EntityFrameworkCore;

    public class CompanyServiceService : ICompanyServiceService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public CompanyServiceService(
            IDeletableEntityRepository<Service> serviceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo)
        {
            this.serviceRepo = serviceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
        }

        public IEnumerable<CompanyServiceOutputModel> GetUserCompanyService(ApplicationUser user)
        {
            var userCompany = this.companyRepo
                .All()
                .Where(x => x.AddedByUserId == user.Id)
                .Include(x => x.Services)
                .ThenInclude(x => x.Vetting)
                .Include(x => x.Services)
                .ThenInclude(x => x.PaidOrder)
                .Include(x => x.Services)
                .ThenInclude(x => x.Packages)
                .Include(x => x.Services)
                .ThenInclude(x => x.Categotry)
                .Include(x => x.Skills)
                .FirstOrDefault();

            if (userCompany == null)
            {
                return null;
            }

            var companyService = new List<CompanyServiceOutputModel>();

            foreach (var item in userCompany.Services)
            {
                var package = new List<PackageModel>();

                foreach (var packageItem in item.Packages)
                {
                    package.Add(new PackageModel()
                    {
                        Price = packageItem.Price,
                        Description = packageItem.Descrtiption,
                    });
                }

                var service = new CompanyServiceOutputModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsPremium = item.IsPremium,
                    Description = item.Description,
                    CategoryName = item.Categotry.Name,
                    Packages = package,
                };

                if (item.PaidOrder != null)
                {
                    service.PaidOrder = new PaidOrderOutputModel()
                    {
                        StartDate = item.PaidOrder.StartDate.ToString(),
                        EndDate = item.PaidOrder.EndDate.ToString(),
                        Name = item.PaidOrder.PaidOrderPackageType.Name,
                        Price = item.PaidOrder.PaidOrderPackageType.Price.ToString(),
                        Terms = item.PaidOrder.PaidOrderPackageType.Terms,
                    };
                }
                else
                {
                    service.PaidOrder = null;
                }

                if (service.Vetting != null)
                {
                    service.Vetting = new VettingOutputModel()
                    {
                        Passed = item.Vetting.Passed,
                        Description = item.Vetting.Description,
                    };
                }
                else
                {
                    service.Vetting = null;
                }


                companyService.Add(service);
            }

            return companyService;
        }

        public async Task CreateAsync(CreateCompanyServiceInputModel input, int id)
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
                    Descrtiption = item.Description,
                });
            }

            var company = this.companyRepo.All().FirstOrDefault(x => x.Id == id);

            company.Services.Add(service);

            await this.companyRepo.SaveChangesAsync();

            // TODO add images
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var service = await this.serviceRepo.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return service;
        }

        public Task<T> GetByIdAsync<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetGategoriesAsync()
        {
            return await this.categoryRepo.All().ToListAsync();
        }

        public async Task UpdateAsync(int id, EditServiceViewModel model)
        {
            var service = await this.serviceRepo.All()
                .Include(x => x.Packages)
                .FirstOrDefaultAsync(x => x.Id == id);

            service.Title = model.Title;
            service.Description = model.Description;
            service.IsPremium = model.IsPremium;
            service.Packages = new List<Package>();
            foreach (var item in model.Packages)
            {
                var package = new Package
                {
                    Price = item.Price,
                    Descrtiption = item.Description,
                };

                service.Packages.Add(package);
            }

            await this.serviceRepo.SaveChangesAsync();
        }
    }
}
