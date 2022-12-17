using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class ImagesMockRepository
    {
        public static Mock<IDeletableEntityRepository<Image>> GetImagesMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Image>>();

            var list = new List<Image>()
            {
                new Image { Id = 1, CompanyId = 1, ServiceId = 1, ImageStorageName = "storage", },
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Image>()))
                .Callback((Image image) => list.Add(image));

            mockRepo.Setup(r => r.Delete(It.IsAny<Image>()))
                .Callback((Image image) => list.Remove(image));

            return mockRepo;
        }
    }
}
