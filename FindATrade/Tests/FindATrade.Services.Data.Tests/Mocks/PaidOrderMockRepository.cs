namespace FindATrade.Services.Data.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using MockQueryable.Moq;
    using Moq;

    public class PaidOrderMockRepository
    {
        public static Mock<IDeletableEntityRepository<PaidOrder>> GetPaidOrderMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<PaidOrder>>();

            var service = new Service() { Id = 1 };

            var list = new List<PaidOrder>()
            {
                new PaidOrder() { Id = 1, Service = service },
            };

            mockRepo.Setup(r => r.All())
                .Returns(new List<PaidOrder>().AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<PaidOrder>()))
                .Callback((PaidOrder paidOrder) => list.Add(paidOrder));

            mockRepo.Setup(r => r.Delete(It.IsAny<PaidOrder>()))
                .Callback((PaidOrder paidOrder) => list.Remove(paidOrder));

            return mockRepo;
        }
    }
}
