using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class LikesMockRepository
    {
        public static Mock<IDeletableEntityRepository<Like>> GetLikesMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Like>>();

            var list = new List<Like>()
            {
                new Like()
                {
                    Id = 1,
                    CompanyId = 1,
                    AddedByUserId = "user",
                },
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Like>()))
                .Callback((Like like) => list.Add(like));

            mockRepo.Setup(r => r.Delete(It.IsAny<Like>()))
                .Callback((Like like) => list.Remove(like));

            return mockRepo;
        }
    }
}
