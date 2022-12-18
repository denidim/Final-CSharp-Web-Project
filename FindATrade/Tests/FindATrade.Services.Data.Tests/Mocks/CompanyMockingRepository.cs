using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class CompanyMockingRepository
    {
        public static Mock<IDeletableEntityRepository<Company>> GetImagesMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Company>>();

            var list = new List<Company>()
            {
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Company>()))
                .Callback((Company company) => list.Add(company));

            mockRepo.Setup(r => r.Delete(It.IsAny<Company>()))
                .Callback((Company company) => list.Remove(company));

            mockRepo.Setup(r => r.HardDelete(It.IsAny<Company>()))
                .Callback((Company company) => list.Remove(company));

            return mockRepo;
        }
    }
}
