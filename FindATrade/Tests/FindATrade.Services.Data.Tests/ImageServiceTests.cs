using System.Linq;
using System.Threading.Tasks;
using FindATrade.Common;
using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using FindATrade.Services.Data.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FindATrade.Services.Data.Tests
{
    public class ImageServiceTests
    {
        private readonly ImageService imageService;
        private readonly Mock<IDeletableEntityRepository<Image>> imagesRepo;
        private readonly Mock<IDeletableEntityRepository<Service>> serviceRepo;
        private readonly ICloudStorageService cloudSorageRepo;

        public ImageServiceTests()
        {
            // Arrange
            this.imagesRepo = ImagesMockRepository.GetImagesMockRepo();
            this.serviceRepo = ServiceMockRepository.GetServiceMockRepo();
            this.cloudSorageRepo = new FakeClodStorageRepo();
            this.imageService = new ImageService(this.imagesRepo.Object, this.serviceRepo.Object, this.cloudSorageRepo);
        }

        [Fact]
        public async Task GenerateSingleImageUrlForCompany_ShouldReturn_Ok()
        {
            // Act
            string cloudResponse = await this.imageService.GenerateSingleImageUrlForCompanyAsync(1);

            // Assert
            Assert.NotNull(cloudResponse);
            Assert.Equal("ok", cloudResponse);
        }

        [Fact]
        public async Task GenerateSingleImageUrlForCompany_ShouldReturn_DefaultImgConst()
        {
            // Act
            string cloudResponse = await this.imageService.GenerateSingleImageUrlForCompanyAsync(2);

            // Assert
            Assert.Equal(ImageConstants.DefaultImage, cloudResponse);
        }
        

        [Fact]
        public async Task GenerateImageUrlsForService_ShouldReturn_Ok()
        {
            // Act
            var cloudResponse = await this.imageService.GenerateImageUrlsForServiceAsync(1);

            // Assert
            Assert.Equal("ok", cloudResponse.First());
        }

        [Fact]
        public async Task GetAllPicturesAsync_ShouldReturn_AllPicturesModel()
        {
            // Act
            var cloudResponse = await this.imageService.GetAllPicturesAsync(1);

            // Assert
            Assert.Equal("storage" ,cloudResponse.First().Name);
            Assert.Equal(1 , cloudResponse.First().ServiceId);
        }

        [Fact]
        public async Task GetAllPicturesAsync_ShouldReturn_Null()
        {
            // Act
            var cloudResponse = await this.imageService.GetAllPicturesAsync(2);

            // Assert
            Assert.Equal(0,cloudResponse.Count());
        }
        
    }
}
