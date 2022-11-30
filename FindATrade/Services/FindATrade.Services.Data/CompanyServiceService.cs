namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.EntityFrameworkCore;

    public class CompanyServiceService : ICompanyServiceService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Package> packagerepo;

        public CompanyServiceService(
            IDeletableEntityRepository<Service> serviceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Package> packagerepo)
        {
            this.serviceRepo = serviceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
            this.packagerepo = packagerepo;
        }

        public IEnumerable<CompanyServiceOutputModel> GetAllCompanyServices(params object[] objects)
        {
            int companyId = -1;

            string userId = string.Empty;

            var obj = objects.First();

            if (obj is int)
            {
                companyId = (int)obj;
            }
            else
            {
                userId = (string)obj;
            }

            var userCompany = this.companyRepo
                .All()
                .Where(x => x.AddedByUserId == userId || x.Id == companyId)
                .Include(x => x.Services)
                .ThenInclude(x => x.Vetting)
                .Include(x => x.Services)
                .ThenInclude(x => x.PaidOrder)
                .Include(x => x.Services)
                .ThenInclude(x => x.Packages)
                .Include(x => x.Services)
                .ThenInclude(x => x.Category)
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
                        Description = packageItem.Description,
                    });
                }

                var service = new CompanyServiceOutputModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsPremium = item.IsPremium,
                    Description = item.Description,
                    CategoryName = item.Category.Name,
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

                if (item.Vetting != null)
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
            var vetting = new Vetting()
            {
                StartVetting = DateTime.UtcNow,
                ApprovalDate = default(DateTime),
                Passed = false,
                Description = "Vetting in progress",
            };

            var service = new Service
            {
                Title = input.Title,

                // TODO: Add Premium Logic
                Description = input.Description,
                CategoryId = input.CategoryId,
                Vetting = vetting,
            };

            foreach (var item in input.Packages)
            {
                service.Packages.Add(new Package
                {
                    Price = item.Price,
                    Description = item.Description,
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

            var package = this.packagerepo.All().Where(x => x.Service == service).ToList();

            foreach (var item in package)
            {
                service.Packages.Remove(item);

                this.packagerepo.Delete(item);
                await this.packagerepo.SaveChangesAsync();
            }

            service.Title = model.Title;
            service.Description = model.Description;
            service.IsPremium = model.IsPremium;
            foreach (var item in model.Packages.Where(x => x.Price != null && x.Description != null))
            {
                var newPackage = new Package
                {
                    Price = item.Price,
                    Description = item.Description,
                };

                service.Packages.Add(newPackage);
            }

            await this.serviceRepo.SaveChangesAsync();
        }
    }
}
