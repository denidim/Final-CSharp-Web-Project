using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class RatingMockRepository
    {
        public static Mock<IDeletableEntityRepository<Rating>> GetRatingsMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Rating>>();

            var list = new List<Rating>();

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Rating>()))
                .Callback((Rating rating) => list.Add(rating));

            mockRepo.Setup(r => r.Delete(It.IsAny<Rating>()))
                .Callback((Rating rating) => list.Remove(rating));

            return mockRepo;
        }
    }
}
