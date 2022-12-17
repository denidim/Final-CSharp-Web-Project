namespace FindATrade.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepo;
        private readonly IDeletableEntityRepository<Service> serviceRepo;
        private readonly ICloudStorageService cloudStorageService;

        public ImageService(
            IDeletableEntityRepository<Image> imageRepo,
            IDeletableEntityRepository<Service> serviceRepo,
            ICloudStorageService cloudStorageService)
        {
            this.imageRepo = imageRepo;
            this.serviceRepo = serviceRepo;
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
                return ImageConstants.DefaultImage;
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
                    ServiceId = serviceId,
                })
                .ToListAsync();
        }

        public async Task CloudDelete(string name)
        {
            await this.cloudStorageService.DeleteFileAsync(name);
        }

        public async Task Delete(string name)
        {
            var image = await this.imageRepo.All()
                .FirstOrDefaultAsync(x => x.ImageStorageName == name);

            if (image == null)
            {
                throw new ArgumentNullException("No image with such name");
            }

            this.imageRepo.HardDelete(image);

            await this.imageRepo.SaveChangesAsync();
        }

        public async Task Add(AddImages images, int id)
        {
            if (images != null)
            {
                foreach (var image in images.Images)
                {
                    var newImage = new Image();
                    newImage.ImageStorageName = ImageNameGenerator.GenerateFileName(image.Name);
                    newImage.ImageUrl = await this.cloudStorageService
                        .UploadFileAsync(image, newImage.ImageStorageName);

                    var servcie = await this.serviceRepo.All()
                        .Include(x => x.Images)
                        .SingleOrDefaultAsync(x => x.Id == id);

                    servcie.Images.Add(newImage);
                }

                await this.serviceRepo.SaveChangesAsync();
            }
        }
    }
}
