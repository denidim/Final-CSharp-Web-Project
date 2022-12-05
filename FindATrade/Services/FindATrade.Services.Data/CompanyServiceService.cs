namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Common;
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
        private readonly IDeletableEntityRepository<Image> imageRepo;
        private readonly IVettingService vettingService;
        private readonly ICloudStorageService cloudStorageService;

        public CompanyServiceService(
            IDeletableEntityRepository<Service> serviceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Package> packagerepo,
            IDeletableEntityRepository<Image> imageRepo,
            IVettingService vettingService,
            ICloudStorageService cloudStorageService)
        {
            this.serviceRepo = serviceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
            this.packagerepo = packagerepo;
            this.imageRepo = imageRepo;
            this.vettingService = vettingService;
            this.cloudStorageService = cloudStorageService;
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

            if (input.Packages != null)
            {
                foreach (var item in input.Packages)
                {
                    service.Packages.Add(new Package
                    {
                        Price = item.Price,
                        Description = item.Description,
                    });
                }
            }

            if (input.Images != null)
            {
                foreach (var image in input.Images)
                {
                    var newImage = new Image();
                    newImage.ImageStorageName = ImageNameGenerator.GenerateFileName(image.Name);
                    newImage.ImageUrl = await this.cloudStorageService
                        .UploadFileAsync(image, newImage.ImageStorageName);

                    service.Images.Add(newImage);
                }
            }

            var company = this.companyRepo.All().FirstOrDefault(x => x.Id == id);

            company.Services.Add(service);

            await this.companyRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditServiceViewModel input)
        {
            var service = await this.serviceRepo.All()
                .Include(x => x.Packages)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            service.Title = input.Title;
            service.Description = input.Description;
            service.IsPremium = input.IsPremium;

            if (service.Packages.Any())
            {
                var package = this.packagerepo
                .All()
                .Where(x => x.Service == service)
                .ToList();

                foreach (var item in package)
                {
                    this.packagerepo.HardDelete(item);
                }

                await this.packagerepo.SaveChangesAsync();
            }

            if (input.Packages != null)
            {
                foreach (var item in input.Packages.Where(x => x.Price != null && x.Description != null))
                {
                    var newPackage = new Package
                    {
                        Price = item.Price,
                        Description = item.Description,
                    };

                    service.Packages.Add(newPackage);
                }
            }

            if (input.Images != null)
            {
                foreach (var image in input.Images)
                {
                    var newImage = new Image();
                    newImage.ImageStorageName = ImageNameGenerator.GenerateFileName(image.Name);
                    newImage.ImageUrl = await this.cloudStorageService
                        .UploadFileAsync(image, newImage.ImageStorageName);

                    service.Images.Add(newImage);
                }
            }

            await this.serviceRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetGategoriesAsync()
        {
            return await this.categoryRepo.All().ToListAsync();
        }

        public IEnumerable<int> GetAllForVettingIds()
        {
            return this.serviceRepo.All()
                .Include(x => x.Vetting)
                .Where(x => x.Vetting.Passed == false)
                .Select(x => x.Id)
                .ToList();
        }

        public bool IsUsersCompany(int serviceId, string userId)
        {
            return this.companyRepo.All()
                .Any(x => x.Services.Any(x => x.Id == serviceId)
                && x.AddedByUserId == userId);
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.serviceRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyServiceByCategoryModel>> GetAllByCategory(string categoryName)
        {
            var output = new List<CompanyServiceByCategoryModel>();

            var service = await this.serviceRepo.All()
                .Include(x => x.Images)
                .Where(x => x.Category.Name == categoryName)
                .ToListAsync();

            foreach (var item in service)
            {
                var newService = new CompanyServiceByCategoryModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                };

                if (item.Images != null && item.Images.Count > 0)
                {
                    newService.OutputImageUrl = await this.cloudStorageService
                        .GetSignedUrlAsync(item.Images.First().ImageStorageName);
                }
                else
                {
                    newService.OutputImageUrl = "https://images.pexels.com/photos/617278/pexels-photo-617278.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";
                }

                output.Add(newService);
            }

            return output;
        }

        public async Task<IEnumerable<CompanyServiceOutputModel>> GetAllByUserIdOrCompanyId(params object[] objects)
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
                .Include(x => x.Services)
                .ThenInclude(x => x.Images)
                .FirstOrDefault();

            if (userCompany == null || userCompany.Services.Count() < 1)
            {
                return null;
            }

            var companyService = new List<CompanyServiceOutputModel>();

            foreach (var service in userCompany.Services)
            {
                var images = new List<string>();

                if (service.Images != null && service.Images.Count > 0)
                {
                    string singleImage = await this.cloudStorageService
                            .GetSignedUrlAsync(service.Images.First().ImageStorageName);

                    images.Add(singleImage);
                }
                else
                {
                    images.Add("https://images.pexels.com/photos/617278/pexels-photo-617278.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1");
                }

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
                    Images = images,
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

                companyService.Add(newService);
            }

            return companyService;
        }
    }
}
