namespace FindATrade.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class FakeVettingService : IVettingService
    {
        public Task<T> GetByServiceIdAsync<T>(int id)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CompanyServicesServiceTests
    {
        private readonly ICompanyServiceService companyServiceService;
        private readonly Mock<IDeletableEntityRepository<Service>> companyServiceRepo = ServiceMockRepository.GetServiceMockRepo();
        private readonly Mock<IDeletableEntityRepository<Category>> categoryRepo = new Mock<IDeletableEntityRepository<Category>>();
        private readonly Mock<IDeletableEntityRepository<Company>> companyRepo = CompanyMockRepository.GetCompanyMockRepo();
        private readonly Mock<IDeletableEntityRepository<Package>> packageRepo = new Mock<IDeletableEntityRepository<Package>>();
        private readonly Mock<IDeletableEntityRepository<Image>> imageRepo = new Mock<IDeletableEntityRepository<Image>>();
        private readonly Mock<IDeletableEntityRepository<Vetting>> vettingRepo = new Mock<IDeletableEntityRepository<Vetting>>();
        private readonly Mock<IDeletableEntityRepository<PaidOrder>> paidOrderRepo = new Mock<IDeletableEntityRepository<PaidOrder>>();
        private readonly IVettingService vettingService;
        private readonly ICloudStorageService cloudStorageService;

        public CompanyServicesServiceTests()
        {
            this.vettingService = new FakeVettingService();
            this.cloudStorageService = new FakeClodStorageRepo();
            this.companyServiceService = new CompanyServcieService(
                this.companyServiceRepo.Object,
                this.categoryRepo.Object,
                this.companyRepo.Object,
                this.packageRepo.Object,
                this.imageRepo.Object,
                this.vettingRepo.Object,
                this.paidOrderRepo.Object,
                this.vettingService,
                this.cloudStorageService);
        }

        [Fact]
        public async Task CreateAsync_Should_Return()
        {
            // Arrange
            var file = new Mock<IFormFile>();

            var servcie = new CreateCompanyServiceInputModel
            {
                Title = $"NewServcie",
                Description = $"This is Servcie N1 description description description description description ",
                CategoryId = 1,
                Packages = new System.Collections.Generic.List<PackageModel>
                {
                    new PackageModel
                    {
                        Price = 1,
                        Description = $"This is package N1 description description description description",
                    },
                },
                Images = new List<IFormFile>()
                {
                    file.Object,
                },
            };

            // Act
            await this.companyServiceService.CreateAsync(servcie, 1);

            // Assert
            var count = this.companyServiceRepo.Object.All().Count();
            Assert.Equal(2, count);
        }
    }
}
