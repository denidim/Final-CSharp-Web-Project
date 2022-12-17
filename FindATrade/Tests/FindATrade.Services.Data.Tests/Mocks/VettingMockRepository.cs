using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class VettingMockRepository
    {
        public static Mock<IDeletableEntityRepository<Vetting>> GetVettingRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Vetting>>();

            var service = new Service() { Id = 1 };

            List<Vetting> list = new List<Vetting>()
            {
                new Vetting()
                {
                    Id = 1,
                    Passed = true,
                    Description = "description",
                    Service = service,
                },
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Vetting>()))
                .Callback((Vetting vetting) => list.Add(vetting));

            mockRepo.Setup(r => r.Delete(It.IsAny<Vetting>()))
                .Callback((Vetting vetting) => list.Remove(vetting));

            return mockRepo;
        }
    }
}
