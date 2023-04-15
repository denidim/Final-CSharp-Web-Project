namespace FindATrade.Services.Data.Tests
{
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using FindATrade.Web.ViewModels.Review;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class RatingServiceTests
    {
        private readonly  RatingService ratingService;
        private readonly Mock<IDeletableEntityRepository<Rating>> ratingRepo;

        public RatingServiceTests()
        {
            // Arrange
            this.ratingRepo = RatingMockRepository.GetRatingsMockRepo();
            this.ratingService = new RatingService(this.ratingRepo.Object);
        }

        [Fact]
        public async Task UserShouldVoteOnlyOnesForTheSameCompany()
        {
            // Act
            await this.ratingService.CreateReviewAsync(new ReviewModel(), 1, "User");
            await this.ratingService.CreateReviewAsync(new ReviewModel(), 1, "User");

            // Assert
            var count = this.ratingRepo.Object.All().Count();
            var userId = this.ratingRepo.Object.All().Single().AddedByUserId;
            Assert.True(count == 1);
            Assert.Equal("User", userId);
        }

        [Fact]
        public async void GetOverallRatingShouldReturnNullIfRatingDoNotExists()
        {
            // Act
            var overall = await this.ratingService.GetOverallRating(1);

            // Assert
            Assert.Null(overall);
        }

        [Fact]
        public async void GetOverallRatingShouldReturnOverallCompanyRatingIfCompanyHasRating()
        {
            // Arrange
            await this.ratingService.CreateReviewAsync(new ReviewModel(), 1, "User");

            // Act
            var overall = await this.ratingService.GetOverallRating(1);

            // Assert
            Assert.NotNull(overall);
        }
    }
}
