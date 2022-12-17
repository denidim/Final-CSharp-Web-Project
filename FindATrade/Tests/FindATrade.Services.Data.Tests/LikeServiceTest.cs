namespace FindATrade.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using Moq;
    using Xunit;

    public class LikeServiceTest
    {
        private readonly LikeService likeService;
        private readonly Mock<IDeletableEntityRepository<Like>> likesRepo;

        public LikeServiceTest()
        {
            // Arrange
            this.likesRepo = LikesMockRepository.GetLikesMockRepo();
            this.likeService = new LikeService(this.likesRepo.Object);
        }

        [Fact]
        public async Task GetLikeCount_ShouldBe_MoreThan_0()
        {
            // Act
            int count = await this.likeService.GetLikeCountAsync(1);

            // Assert
            Assert.True(count > 0);
        }

        [Fact]
        public async Task GetLikeCount_ShouldThrow_ArgumentNullException()
        {
            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.likeService.GetLikeCountAsync(2));
        }

        [Fact]
        public async Task SetLike_ShouldAddLike()
        {
            // Act
            await this.likeService.SetLike(1, "test");
            await this.likeService.SetLike(1, "test");

            // Assert
            var count = this.likesRepo.Object.All().Count();
            var id = this.likesRepo.Object.All().Skip(1).First().AddedByUserId;
            Assert.True(count == 2);
            Assert.Equal("test",  id);
        }
    }
}
