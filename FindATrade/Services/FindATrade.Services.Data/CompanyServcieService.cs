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
    using FindATrade.Web.ViewModels.Subscription;
    using Microsoft.EntityFrameworkCore;

    public class CompanyServcieService : ICompanyServiceService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Package> packagerepo;
        private readonly IDeletableEntityRepository<Image> imageRepo;
        private readonly IDeletableEntityRepository<Vetting> vettingRepo;
        private readonly IDeletableEntityRepository<PaidOrder> paidOrderRepo;
        private readonly IVettingService vettingService;
        private readonly ICloudStorageService cloudStorageService;

        public CompanyServcieService(
            IDeletableEntityRepository<Service> serviceRepo,
            IDeletableEntityRepository<Category> categoryRepo,
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Package> packagerepo,
            IDeletableEntityRepository<Image> imageRepo,
            IDeletableEntityRepository<Vetting> vettingRepo,
            IDeletableEntityRepository<PaidOrder> paidOrderRepo,
            IVettingService vettingService,
            ICloudStorageService cloudStorageService)
        {
            this.serviceRepo = serviceRepo;
            this.categoryRepo = categoryRepo;
            this.companyRepo = companyRepo;
            this.packagerepo = packagerepo;
            this.imageRepo = imageRepo;
            this.vettingRepo = vettingRepo;
            this.paidOrderRepo = paidOrderRepo;
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
                Description = VettingConstants.Progress,
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

            var packages = this.packagerepo
                .All()
                .Where(x => x.Service == service)
                .ToList();

            for (int i = 0; i < input.Packages.Count; i++)
            {
                packages[i].Description = input.Packages[i].Description;
                packages[i].Price = input.Packages[i].Price;
            }

            await this.packagerepo.SaveChangesAsync();

            await this.serviceRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await this.serviceRepo.All()
                .Where(x => x.Id == id)
                .Include(x => x.Packages)
                .Include(x => x.Images)
                .Include(x => x.PaidOrder)
                .Include(x => x.Vetting)
                .SingleOrDefaultAsync();

            if (service.Vetting != null)
            {
                this.vettingRepo.Delete(service.Vetting);
            }

            if (service.PaidOrder != null)
            {
                this.paidOrderRepo.Delete(service.PaidOrder);
            }

            if (service.Images != null)
            {
                foreach (var image in service.Images)
                {
                    this.imageRepo.Delete(image);
                }
            }

            if (service.Packages != null)
            {
                foreach (var package in service.Packages)
                {
                    this.packagerepo.Delete(package);
                }
            }

            this.serviceRepo.Delete(service);

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

        public bool IsUsersService(string userId, int serviceId)
        {
            return this.serviceRepo.All()
                .Where(x => x.Id == serviceId)
                .Include(x => x.Company)
                .Any(x => x.Company.AddedByUserId == userId);
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.serviceRepo
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyServiceByCategoryOutputModel>> GetAllByCategory(string categoryName)
        {
            var output = new List<CompanyServiceByCategoryOutputModel>();

            var service = await this.serviceRepo.All()
                .Include(x => x.Images)
                .Where(x => x.Category.Name == categoryName && x.Vetting.Passed == true)
                .ToListAsync();

            foreach (var item in service)
            {
                var newService = new CompanyServiceByCategoryOutputModel
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
                    newService.OutputImageUrl = ImageConstants.DefaultImage;
                }

                output.Add(newService);
            }

            return output;
        }

        public async Task<IEnumerable<SingleServiceOutputModel>> GetAllByUserIdOrCompanyId(params object[] objects)
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

            var services = await this.serviceRepo.All()
                .Where(x => x.Company.AddedByUserId == userId || x.Company.Id == companyId)
                .Include(x => x.Category)
                .ToListAsync();

            if (!services.Any() || services == null)
            {
                return null;
            }

            var companyService = new List<SingleServiceOutputModel>();

            foreach (var service in services)
            {
                var images = new List<string>();

                var serviceImages = await this.imageRepo.All()
                    .Where(x => x.ServiceId == service.Id)
                    .ToListAsync();

                if (serviceImages.Any() && serviceImages != null)
                {
                    string singleImage = await this.cloudStorageService
                            .GetSignedUrlAsync(service.Images.First().ImageStorageName);

                    images.Add(singleImage);
                }
                else
                {
                    images.Add(ImageConstants.DefaultImage);
                }

                var servicePackages = await this.packagerepo.All()
                    .Where(x => x.ServiceId == service.Id)
                    .Select(x => new PackageModel
                    {
                        Price = x.Price,
                        Description = x.Description,
                    })
                    .ToListAsync();

                var servicePaidOreders = await this.paidOrderRepo.All()
                    .Where(x => x.Service == service)
                    .Select(x => new SubscriptionModel
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        Name = x.Name,
                        Price = x.Price.ToString(),
                        Terms = x.Terms,
                    })
                    .FirstOrDefaultAsync();

                var newService = new SingleServiceOutputModel()
                {
                    Id = service.Id,
                    Title = service.Title,
                    IsPremium = service.IsPremium,
                    Description = service.Description,
                    CategoryName = service.Category.Name,
                    Packages = servicePackages,
                    Images = images,
                    Subscription = servicePaidOreders,
                };

                newService.Vetting = await this.vettingService.GetByServiceIdAsync<VettingOutputModel>(service.Id);

                companyService.Add(newService);
            }

            return companyService;
        }
    }
}
