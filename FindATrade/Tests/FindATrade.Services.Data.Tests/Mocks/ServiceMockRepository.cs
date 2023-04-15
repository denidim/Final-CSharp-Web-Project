namespace FindATrade.Services.Data.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using MockQueryable.Moq;
    using Moq;

    public class ServiceMockRepository
    {
        public static Mock<IDeletableEntityRepository<Service>> GetServiceMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Service>>();

            var list = new List<Service>()
            {
                new Service()
                {
                    Id = 1,
                    Title = "Title",
                    CategoryId = 1,
                    Category = new Category
                    {
                        Id = 1,
                        Name = "Category",
                    },
                    CompanyId = 1,
                    Images = new List<Image>()
                    {
                        new Image(),
                    },
                    Packages = new List<Package>()
                    {
                        new Package(),
                    },
                    PaidOrderId = 1,
                    PaidOrder = new PaidOrder(),
                    VettingId = 1,
                    Vetting = new Vetting(),
                },
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Service>()))
                .Callback((Service service) => list.Add(service));

            mockRepo.Setup(r => r.Delete(It.IsAny<Service>()))
                .Callback((Service service) => list.Remove(service));

            mockRepo.Setup(r => r.HardDelete(It.IsAny<Service>()))
                .Callback((Service service) => list.Remove(service));

            return mockRepo;
        }
    }
}
