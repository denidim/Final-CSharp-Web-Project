namespace FindATrade.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data.Tests.Mocks;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.AspNetCore.Http;
    using MockQueryable.Moq;
    using Moq;
    using Xunit;

    public class CompanyServiceTests
    {
        private readonly ICompanyService companyService;
        private readonly Mock<IDeletableEntityRepository<Company>> companyRepo = CompanyMockRepository.GetCompanyMockRepo();
        private readonly Mock<IDeletableEntityRepository<Skill>> skillRepo = SkillMockRepository.GetSkillMockRepo();
        private readonly ICloudStorageService cloudStorageService;
        private readonly Mock<IDeletableEntityRepository<Image>> imageRepo = new Mock<IDeletableEntityRepository<Image>>();
        private readonly Mock<IDeletableEntityRepository<Address>> addressRepo = new Mock<IDeletableEntityRepository<Address>>();
        private readonly Mock<IDeletableEntityRepository<Rating>> ratingRepo = new Mock<IDeletableEntityRepository<Rating>>();
        private readonly Mock<IDeletableEntityRepository<Like>> likeRepo = new Mock<IDeletableEntityRepository<Like>>();

        public CompanyServiceTests()
        {
            // Arrange
            this.cloudStorageService = new FakeClodStorageRepo();
            this.companyService = new CompanyService(
                this.companyRepo.Object,
                this.skillRepo.Object,
                this.cloudStorageService,
                this.imageRepo.Object,
                this.addressRepo.Object,
                this.ratingRepo.Object,
                this.likeRepo.Object);
        }

        [Fact]
        public async Task Create_Shuld_Be_Equal()
        {
            // Arrange
            var company = new CreateCompanyInputModel
            {
                Name = $"NewCompany",
                Website = $"Website.com",
                Email = $"ComapnyEmail@email.com",
                PhoneNumber = "1234567",
                Description = $"This is Company N description description description description description",
                Address = new CreateCompanyAddressInputModel
                {
                    Street = $"Street1",
                    HouseNumber = 1,
                    City = $"City",
                    Country = $"Country",
                },
                Skills = new System.Collections.Generic.List<SkillModel>
                    {
                        new SkillModel
                        {
                            Name = $"skill",
                        },
                        new SkillModel
                        {
                            Name = $"skill",
                        },
                        new SkillModel
                        {
                            Name = $"skill",
                        },
                    },
            };

            var user = new ApplicationUser()
            {
                Id = "user",
            };

            // Act
            await this.companyService.CreateAsync(company, user);

            // Assert
            var count = this.companyRepo.Object.All().Count();
            var street = this.companyRepo.Object.All().Skip(1).First().Address.Street;
            Assert.Equal(2, count);
            Assert.Equal("Street1", street);
        }

        [Fact]
        public async Task Upadete_Shuld_Be_Equal()
        {
            // Arrange
            var file = new Mock<IFormFile>();

            var model = new EditCompanyViewModel()
            {
                Id = 1,
                Name = $"EditedCompany",
                Website = $"Website.com",
                Email = $"ComapnyEmail@email.com",
                PhoneNumber = "1234567",
                Description = $"This is Company N description description description description description",
                Image = file.Object,
                Address = new CreateCompanyAddressInputModel
                {
                    Street = $"Street",
                    HouseNumber = 1,
                    City = $"City",
                    Country = $"Country",
                },
                Skills = new System.Collections.Generic.List<SkillModel>
                    {
                        new SkillModel
                        {
                            Name = $"skill1",
                        },
                        new SkillModel
                        {
                            Name = $"skill",
                        },
                        new SkillModel
                        {
                            Name = $"skill",
                        },
                    },
            };

            // Act
            await this.companyService.UpdateAsync(1, model);

            // Assert
            var company = this.companyRepo.Object.All().First();
            var skills = this.skillRepo.Object.All().First();
            Assert.Equal("EditedCompany", company.Name);
            Assert.Equal("skill1", skills.Name);
        }

        [Fact]
        public async Task Delete_Should_Be_Equal()
        {
            // Arrange
            List<Company> companyList = new List<Company>()
            {
                new Company
                {
                    Id = 1,
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
                            CompanyId= 1,
                            Name = $"skill",
                        },
                        new Skill
                        {
                            CompanyId= 1,
                            Name = $"skill",
                        },
                        new Skill
                        {
                            CompanyId= 1,
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
                },
            };
            this.companyRepo.Setup(r => r.All()).Returns(companyList.AsQueryable().BuildMock());
            this.companyRepo.Setup(r => r.HardDelete(It.IsAny<Company>()))
                                .Callback((Company company) => companyList.Remove(company));

            // Act
            await this.companyService.DeleteAsync(1);

            // Assert
            var count = this.companyRepo.Object.All().Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_ShouldThrow_ArgumentException()
        {
            // Assert
            await Assert.ThrowsAsync<System.ArgumentException>(() => this.companyService.DeleteAsync(1));
        }

        [Fact]
        public void IsCompanyUser_Should_Return_True()
        {
            // Assert
            Assert.True(this.companyService.IsUsersCompany("user", 1));
        }

        [Fact]
        public void IsCompanyUser_Should_Return_False()
        {
            // Assert
            Assert.False(this.companyService.IsUsersCompany("false", 1));
        }

        [Fact]
        public async Task GetCompanyByIdAsync_Should_Return_NotNull()
        {
            // Act
            var company = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(1);

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_Should_Return_Null()
        {
            // Arrange
            AutoMapperConfig.RegisterMappings(typeof(CompanyOutputModel).GetTypeInfo().Assembly);
            List<Company> companyList = new List<Company>()
            {
                 new Company
                {
                    Id = 1,
                    Services = new System.Collections.Generic.List<Service>
                    {
                        new Service()
                        {
                            Vetting = new Vetting()
                            {
                                Passed = false,
                            },
                        },
                    },
                },
            };
            this.companyRepo.Setup(r => r.All()).Returns(companyList.AsQueryable().BuildMock());

            // Act
            var company = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(1);

            // Assert
            Assert.Null(company);
        }

        [Fact]
        public async Task GetForEdit_Should_Return_NotNull()
        {
            // Act
            var company = await this.companyService.GetForEditCompanyByIdAsync<EditCompanyViewModel>(1);

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetCompanyByServiceId_Should_Return_NotNull()
        {
            // Act
            var company = await this.companyService.GetCompanyByServiceId(1);

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetCompanyByUserIdAsync_Should_Return_NotNull()
        {
            // Act
            var company = await this.companyService.GetCompanyByUserIdAsync<CompanyOutputModel>("user");

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetAll_Should_Return_Single()
        {
            // Act
            var output = await this.companyService.GetAll(1, 2);

            // Assert
            Assert.Single(output);
        }

        [Fact]
        public async Task GetSubscribed_Should_Return_Single()
        {
            // Act
            var output = await this.companyService.GetSubscribed();

            // Assert
            Assert.Single(output);
        }

        [Fact]
        public void GetCount_Should_Return_1()
        {
            // Act
            var output = this.companyService.GetCount();

            // Assert
            Assert.Equal(1, output);
        }
    }
}
