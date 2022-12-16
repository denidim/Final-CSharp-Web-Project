namespace FindATrade.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.AspNetCore.Identity;

    public class SeederService : ISeederServcie
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyServiceService companyServiceService;
        private readonly ICompanyService companyService;
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public SeederService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Service> serviceRepo,
            ICompanyServiceService companyServiceService,
            ICompanyService companyService,
            IDeletableEntityRepository<Company> companyRepo)
        {
            this.userManager = userManager;
            this.companyServiceService = companyServiceService;
            this.companyService = companyService;
            this.companyRepo = companyRepo;
        }

        public async Task SeedAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var user = new ApplicationUser()
                {
                    Email = $"UserEmail{i}@emial.com",
                    EmailConfirmed = true,
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    UserName = $"UserName{i}",
                };

                var result = await this.userManager.CreateAsync(user, "User123!");

                await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                var company = new CreateCompanyInputModel
                {
                    Name = $"Company{i}",
                    Website = $"Website{i}.com",
                    Email = $"ComapnyEmail{i}@email.com",
                    PhoneNumber = "1234567",
                    Description = $"This is Company N{i} description description description description description",
                    Address = new CreateCompanyAddressInputModel
                    {
                        Street = $"Street{i}",
                        HouseNumber = i,
                        City = $"City{i}",
                        Country = $"Country{i}",
                    },
                    Skills = new System.Collections.Generic.List<SkillModel>
                    {
                        new SkillModel
                        {
                            Name = $"skill{i}",
                        },
                        new SkillModel
                        {
                            Name = $"skill{i}",
                        },
                        new SkillModel
                        {
                            Name = $"skill{i}",
                        },
                    },
                };

                await this.companyService.CreateAsync(company, user);

                var companyId = this.companyRepo.All().FirstOrDefault(x => x.Email == company.Email).Id;

                for (int j = 0; j < 2; j++)
                {
                    var servcie = new CreateCompanyServiceInputModel
                    {
                        Title = $"Servcie{j}",
                        Description = $"This is Servcie N{j} description description description description description ",
                        CategoryId = 1,
                        Packages = new System.Collections.Generic.List<PackageModel>
                        {
                            new PackageModel
                            {
                                Price = j,
                                Description = $"This is package N {j} description description description description",
                            },
                        },
                    };

                    await this.companyServiceService.CreateAsync(servcie, companyId);
                }
            }
        }

    }
}
