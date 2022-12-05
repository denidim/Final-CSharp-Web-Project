namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepo;
        private readonly ICloudStorageService cloudStorageService;

        public ImageService(
            IDeletableEntityRepository<Image> imageRepo,
            ICloudStorageService cloudStorageService)
        {
            this.imageRepo = imageRepo;
            this.cloudStorageService = cloudStorageService;
        }

        public async Task<string> GenerateSingleImageUrlForCompany(int companyId)
        {
            // Get the sorage name from company image
            var savedImageName = await this.imageRepo.All()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.ImageStorageName)
                .SingleOrDefaultAsync();

            if (savedImageName == null)
            {
                return null;
            }

            // creates new Url with exparation to show to the outside world
            return await this.cloudStorageService.GetSignedUrlAsync(savedImageName);
        }

        public async Task<IEnumerable<string>> GenerateImageUrlsForService(int serviceId)
        {
            // Get the sorage name from company image
            var savedImageName = await this.imageRepo.All()
                .Where(x => x.ServiceId == serviceId)
                .Select(x => x.ImageStorageName)
                .ToListAsync();
            if (!savedImageName.Any())
            {
                return null;
            }

            var urls = new List<string>();

            foreach (var item in savedImageName)
            {
                // creates new Url with exparation to show to the outside world
                urls.Add(await this.cloudStorageService.GetSignedUrlAsync(item));
            }

            return urls;
        }

        public async Task<IEnumerable<AllPicturesModel>> GetAllPictures(int serviceId)
        {
            return await this.imageRepo.All()
                .Where(x => x.ServiceId == serviceId)
                .Select(x => new AllPicturesModel
                {
                    Name = x.ImageStorageName,
                    Url = x.ImageUrl,
                })
                .ToListAsync();
        }

        public async Task Delete(string name)
        {
            await this.cloudStorageService.DeleteFileAsync(name);
        }
    }
}
