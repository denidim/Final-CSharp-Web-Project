namespace FindATrade.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Review;
    using Moq;
    using Xunit;

    public class RatingServiceTests
    {
        [Fact]
        public async Task UserShoudVoteOnlyOnesForTheSameCompany()
        {
            var ratings = new List<Rating>();

            var mockRepo = new Mock<IDeletableEntityRepository<Rating>>();

            mockRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());

            mockRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rating) => ratings.Add(rating));

            var service = new RatingService(mockRepo.Object);

            await service.CreateReviewAsync(new ReviewModel(), 1, "User");
            await service.CreateReviewAsync(new ReviewModel(), 1, "User");

            Assert.Single(ratings);
            Assert.Equal(1, ratings.First().CompanyId);
            Assert.Equal("User", ratings.First().AddedByUserId);
        }

        [Fact]
        public void GetOversllRatingShouldReturnNullIfRatingDoesentExists()
        {
            var ratings = new List<Rating>();

            var mockRepo = new Mock<IDeletableEntityRepository<Rating>>();

            mockRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());

            mockRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rating) => ratings.Add(rating));

            var service = new RatingService(mockRepo.Object);

            var overall = new OverallCompanyRating();

            overall = service.GetOverallRating(1);

            Assert.Null(overall);
        }

        [Fact]
        public async void GetOversllRatingShoulReturnOverallCompanyRatingIfCompanyHasRating()
        {
            var ratings = new List<Rating>();

            var mockRepo = new Mock<IDeletableEntityRepository<Rating>>();

            mockRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());

            mockRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rating) => ratings.Add(rating));

            var service = new RatingService(mockRepo.Object);

            var overall = new OverallCompanyRating();

            await service.CreateReviewAsync(new ReviewModel(), 1, "User");

            overall = service.GetOverallRating(1);

            Assert.NotNull(overall);
        }
    }
}
