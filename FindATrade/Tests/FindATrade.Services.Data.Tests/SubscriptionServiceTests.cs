using System.Linq;

namespace FindATrade.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Subscription;
    using Moq;
    using Xunit;

    public class SubscriptionServiceTests
    {
        private readonly SubscriptionService subscriptionService;
        private readonly Mock<IDeletableEntityRepository<PaidOrder>> paidOrderRepo;
        private readonly Mock<IDeletableEntityRepository<Service>> serviceRepo;

        public SubscriptionServiceTests()
        {
            // Arrange
            this.paidOrderRepo = PaidOrderMockRepository.GetPaidOrderMockRepo();
            this.serviceRepo = ServiceMockRepository.GetServiceMockRepo();
            this.subscriptionService = new SubscriptionService(this.paidOrderRepo.Object, this.serviceRepo.Object);
        }

        [Fact]
        public async Task GetPaidOrderAsync_ShouldReturn_SubscriptionModel()
        {
            // Arrange
            AutoMapperConfig.RegisterMappings(typeof(SubscriptionModel).GetTypeInfo().Assembly);

            // Act
            var result = await this.subscriptionService.GetPaidOrderAsync<SubscriptionModel>(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddSubscriptionAsync_ShouldAdd()
        {
            // Act
            await this.subscriptionService.AddSubscriptionAsync(1);

            // Assert
            var count = this.serviceRepo.Object.All().Count();
            var id = this.serviceRepo.Object.All().Single().Id;
            Assert.True(count == 1);
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task AddSubscriptionAsync_ShouldThrow_ArgumentNullException()
        {
            // Act
            await this.subscriptionService.AddSubscriptionAsync(1);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.subscriptionService.AddSubscriptionAsync(2));
        }

        [Fact]
        public async Task RemoveExpiredSubscriptionsAsync()
        {
            // Act
            await this.subscriptionService.RemoveExpiredSubscriptionsAsync(1);

            // Assert
            var count = this.paidOrderRepo.Object.All().Count();

            Assert.True(count == 0);
        }
    }
}
