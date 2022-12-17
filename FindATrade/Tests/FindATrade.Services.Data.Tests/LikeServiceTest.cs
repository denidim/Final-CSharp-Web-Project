namespace FindATrade.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using MockQueryable.Moq;
    using Moq;
    using Xunit;

    public class LikeServiceTest
    {
        private readonly LikeService likeService;
        private readonly Mock<IDeletableEntityRepository<Like>> likesRepo = new Mock<IDeletableEntityRepository<Like>>();

        public LikeServiceTest()
        {
            this.likeService = new LikeService(this.likesRepo.Object);
        }

        [Fact]
        public async Task GetLikeCount_ShouldBe_MoreThan_0()
        {
            // Arange
            var list = new List<Like>()
            {
                new Like()
                {
                    Id = 1,
                    CompanyId = 1,
                },
            };

            this.likesRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable().BuildMock());

            // Act
            int count = await this.likeService.GetLikeCountAsync(1);

            // Assert
            Assert.True(count > 0);
        }

        [Fact]
        public async Task GetLikeCount_ShouldThrow_ArgumentNullException()
        {
            // Arange
            var list = new List<Like>()
            {
                new Like()
                {
                    Id = 1,
                    CompanyId = 1,
                },
            };

            this.likesRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable().BuildMock());

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.likeService.GetLikeCountAsync(2));
        }

        [Fact]
        public async Task SetLike_ShouldAddlike()
        {
            // Arange
            var list = new List<Like>();

            this.likesRepo.Setup(r => r.All()).Returns(list.AsQueryable().BuildMock());

            this.likesRepo.Setup(r => r.AddAsync(It.IsAny<Like>())).Callback((Like like) => list.Add(like));

            // Act
            await this.likeService.SetLike(1, "user");
            await this.likeService.SetLike(1, "user");

            // Assert
            Assert.Single(list);
            Assert.Equal(1, list.First().CompanyId);
            Assert.Equal("user", list.First().AddedByUserId);
        }
    }
}
