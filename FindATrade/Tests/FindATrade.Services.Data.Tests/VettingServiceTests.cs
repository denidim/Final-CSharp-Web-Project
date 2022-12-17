namespace FindATrade.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels;
    using FindATrade.Web.ViewModels.CompanyService;
    using MockQueryable.Moq;
    using Moq;
    using Xunit;

    public class VettingServiceTests
    {
        private readonly VettingService vettingService;
        private readonly Mock<IDeletableEntityRepository<Vetting>> vettingRepo = new Mock<IDeletableEntityRepository<Vetting>>();

        public VettingServiceTests()
        {
            this.vettingService = new VettingService(this.vettingRepo.Object);
        }

        [Fact]
        public async Task GetLikeCount_ShouldBe_MoreThan_0()
        {
            // Arange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var passed = true;
            var description = "vely long description vely long description vely long description vely long description vely long description";

            var service = new Service() { Id = 1 };

            List<Vetting> list = new List<Vetting>()
            {
                new Vetting()
                {
                    Id = 1,
                    Passed = passed,
                    Description = description,
                    Service = service,
                },
            };

            this.vettingRepo.Setup(r => r.All()).Returns(list.AsQueryable().BuildMock());

            // Act
            var result = await this.vettingService.GetByServiceIdAsync<VettingOutputModel>(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Passed);
            Assert.Equal(description, result.Description);
        }
    }
}
