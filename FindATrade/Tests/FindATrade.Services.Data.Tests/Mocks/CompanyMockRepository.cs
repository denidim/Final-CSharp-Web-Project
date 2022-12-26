namespace FindATrade.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using MockQueryable.Moq;
    using Moq;

    public class CompanyMockRepository
    {
        public static Mock<IDeletableEntityRepository<Company>> GetCompanyMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Company>>();

            var list = new List<Company>()
            {
                 new Company
                {
                    Id = 1,
                    AddedByUserId = "user",
                    Name = $"Company",
                    Website = $"Website.com",
                    Email = $"ComapnyEmail@email.com",
                    PhoneNumber = "1234567",
                    Description = $"This is Company N description description description description description",
                    Address = new Address
                    {
                        Street = $"Street",
                        HouseNumber = 1,
                        City = $"City",
                        Country = $"Country",
                    },
                    Skills = new System.Collections.Generic.List<Skill>
                    {
                        new Skill
                        {
                            CompanyId = 1,
                            Name = $"skill",
                        },
                        new Skill
                        {
                            CompanyId = 1,
                            Name = $"skill",
                        },
                        new Skill
                        {
                            CompanyId = 1,
                            Name = $"skill",
                        },
                    },
                    Ratings = new System.Collections.Generic.List<Rating>
                    {
                        new Rating(),
                    },
                    Likes = new System.Collections.Generic.List<Like>
                    {
                        new Like(),
                    },
                    Image = new Image(),
                    Services = new System.Collections.Generic.List<Service>
                    {
                        new Service()
                        {
                            Id = 1,
                            Vetting = new Vetting()
                            {
                                Passed = true,
                            },
                            PaidOrder = new PaidOrder()
                            {
                                EndDate = DateTime.UtcNow,
                            },
                        },
                    },
                },
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
