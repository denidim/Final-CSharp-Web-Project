using FindATrade.Common;
using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using FindATrade.Services.Data.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindATrade.Web.ViewModels.CompanyService;
using Xunit;
using Microsoft.AspNetCore.Http;

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
            Assert.Equal("storage", cloudResponse.First().Name);
            Assert.Equal(1, cloudResponse.First().ServiceId);
        }

        [Fact]
        public async Task GetAllPicturesAsync_ShouldReturn_Null()
        {
            // Act
            var cloudResponse = await this.imageService.GetAllPicturesAsync(2);

            // Assert
            Assert.Empty(cloudResponse);
        }

        [Fact]
        public async Task Delete_ShouldReturn_EmptyCollection()
        {
            // Act
            await this.imageService.Delete("storage");

            // Assert
            var count = this.imagesRepo.Object.All().Count();
            Assert.True(count == 0);
        }

        [Fact]
        public async Task Delete_ShouldReturn_Null()
        {
            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.imageService.Delete(null));
        }

        [Fact]
        public async Task Add_ShouldReturn_Null()
        {
            // Act

            var file = new Mock<IFormFile>();

            var images = new AddImages()
            {
                Images = new List<IFormFile>()
                {
                    file.Object,
                },
            };

            await this.imageService.Add(images, 1);

            // Assert
            var count = this.serviceRepo.Object.All().First().Images.Count();
            Assert.Equal(1,count);
        }
    }
}
