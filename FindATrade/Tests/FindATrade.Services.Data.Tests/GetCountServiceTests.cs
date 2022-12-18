namespace FindATrade.Services.Data.Tests
{
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using System.Linq;
    using MockQueryable.Moq;

    public class GetCountServiceTests
    {
        private readonly IGetCountsService getCountService;
        private readonly Mock<IDeletableEntityRepository<Category>> categoryRepo = new Mock<IDeletableEntityRepository<Category>>();
        private readonly Mock<IDeletableEntityRepository<Company>> companyRepo = new Mock<IDeletableEntityRepository<Company>>();
        private readonly Mock<IDeletableEntityRepository<Service>> serviceRepo = new Mock<IDeletableEntityRepository<Service>>();

        public GetCountServiceTests()
        {
            this.getCountService = new GetCountsService(this.categoryRepo.Object, this.companyRepo.Object, this.serviceRepo.Object);
        }

        [Fact]
        public void GetCountService_ShouldWorkCorrectly()
        {
            // Arrange
            List<Category> categoryList = new List<Category>()
            {
                new Category(),
            };
            this.categoryRepo.Setup(r => r.All()).Returns(categoryList.AsQueryable().BuildMock());

            List<Company> companyList = new List<Company>()
            {
                new Company(),
            };
            this.companyRepo.Setup(r => r.All()).Returns(companyList.AsQueryable().BuildMock());

            List<Service> serviceList = new List<Service>()
            {
                new Service(),
            };
            this.serviceRepo.Setup(r => r.All()).Returns(serviceList.AsQueryable().BuildMock());

            // Act
            var result = this.getCountService.GetCounts();

            // Assert
            Assert.True(result.CategoriesCount == 1);
            Assert.True(result.CompaniesCount == 1);
            Assert.True(result.ServicesCount == 1);
        }
    }
}
