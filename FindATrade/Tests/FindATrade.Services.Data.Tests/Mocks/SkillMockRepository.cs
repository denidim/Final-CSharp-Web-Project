using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class SkillMockRepository
    {
        public static Mock<IDeletableEntityRepository<Skill>> GetSkillMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Skill>>();

            var list = new List<Skill>()
            {
                new Skill()
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
            };

            mockRepo.Setup(r => r.All())
                .Returns(list.AsQueryable().BuildMock());

            return mockRepo;
        }
    }
}
