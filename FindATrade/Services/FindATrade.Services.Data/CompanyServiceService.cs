namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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
        private readonly IVettingService vettingService;

        public CompanyServiceService(
            IDeletableEntityRepository<Service> serviceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Package> packagerepo,
            IVettingService vettingService)
        {
            this.serviceRepo = serviceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
            this.packagerepo = packagerepo;
            this.vettingService = vettingService;
        }

        public IEnumerable<int> GetAllForVettingIds()
        {
            return this.serviceRepo.All()
                .Include(x => x.Vetting)
                .Where(x => x.Vetting.Passed == false)
                .Select(x => x.Id)
                .ToList();
        }

        public async Task<IEnumerable<T>> GetAllByCategory<T>(string categoryName)
        {
            return await this.serviceRepo.All()
                .Where(x => x.Category.Name == categoryName)
                .To<T>()
                .ToListAsync();
        }

        public bool IsUsersCompany(int serviceId, string userId)
        {
            return this.companyRepo.All()
                .Any(x => x.Services.Any(x => x.Id == serviceId)
                && x.AddedByUserId == userId);
        }



        public async Task<IEnumerable<CompanyServiceOutputModel>> GetAllCompanyServices(params object[] objects)
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

            if (userCompany == null || userCompany.Services.Count() < 1)
            {
                return null;
            }

            var companyService = new List<CompanyServiceOutputModel>();

            foreach (var service in userCompany.Services)
            {
                var package = new List<PackageModel>();

                foreach (var packageItem in service.Packages)
                {
                    package.Add(new PackageModel()
                    {
                        Price = packageItem.Price,
                        Description = packageItem.Description,
                    });
                }

                var newService = new CompanyServiceOutputModel()
                {
                    Id = service.Id,
                    Title = service.Title,
                    IsPremium = service.IsPremium,
                    Description = service.Description,
                    CategoryName = service.Category.Name,
                    Packages = package,
                };

                if (service.PaidOrder != null)
                {
                    newService.PaidOrder = new PaidOrderOutputModel()
                    {
                        StartDate = service.PaidOrder.StartDate.ToString(),
                        EndDate = service.PaidOrder.EndDate.ToString(),
                        Name = service.PaidOrder.PaidOrderPackageType.Name,
                        Price = service.PaidOrder.PaidOrderPackageType.Price.ToString(),
                        Terms = service.PaidOrder.PaidOrderPackageType.Terms,
                    };
                }
                else
                {
                    newService.PaidOrder = null;
                }

                newService.Vetting = await this.vettingService.GetByServiceIdAsync<VettingOutputModel>(service.Id);


                //if (service.Vetting != null)
                //{
                //    newService.Vetting = new VettingOutputModel()
                //    {
                //        Passed = service.Vetting.Passed,
                //        Description = service.Vetting.Description,
                //    };
                //}
                //else
                //{
                //    newService.Vetting = null;
                //}

                companyService.Add(newService);
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
