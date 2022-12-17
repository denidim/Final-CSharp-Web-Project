namespace FindATrade.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels;
    using FindATrade.Web.ViewModels.CompanyService;
    using MockQueryable.Moq;
    using Moq;
    using Xunit;

    public class VettingServiceTests
    {
        private readonly VettingService vettingService;
        private readonly Mock<IDeletableEntityRepository<Vetting>> vettingRepo;

        public VettingServiceTests()
        {
            // Arrange
            this.vettingRepo = VettingMockRepository.GetVettingRepo();
            this.vettingService = new VettingService(this.vettingRepo.Object);
        }

        [Fact]
        public async Task GetLikeCount_ShouldBe_MoreThan_0()
        {
            // Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Act
            var result = await this.vettingService.GetByServiceIdAsync<VettingOutputModel>(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Passed);
            Assert.Equal("description", result.Description);
        }
    }
}
