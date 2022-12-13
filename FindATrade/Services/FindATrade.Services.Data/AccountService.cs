namespace FindATrade.Services.Data
{
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.AccountManagement;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.EntityFrameworkCore;

    public class AccountService : IAccountService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Service> serviceRepo;

        public AccountService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Service> serviceRepo)
        {
            this.companyRepo = companyRepo;
            this.serviceRepo = serviceRepo;
        }

        public CompanyOutputModel GetCompanyInfo(ApplicationUser user)
        {
            var company = this.companyRepo
                .All()
                .Where(x => x.AddedByUserId == user.Id)
                .Select(x => new CompanyOutputModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    WebSite = x.Website,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Description = x.Description,
                    Address = $"{x.Address.Street} {x.Address.City}",
                    Likes = x.Likes.Count(),
                    Skills = x.Skills.Select(x => new SkillModel()
                    {
                        Name = x.Name,
                    }).ToList(),
                    Ratings = x.Ratings.Select(x => new CompanyRatingsModel()
                    {
                        Reliability = x.Reliability,
                        Description = x.Description,
                        Courtesy = x.Courtesy,
                        QuoteAccuracy = x.QuoteAccuracy,
                        Tidiness = x.Tidiness,
                        Workmanship = x.Workmanship,
                    }).ToList(),
                }).FirstOrDefault();

            return company;
        }

        public T GetCompanyInfoByUser<T>(ApplicationUser user)
        {
            var compnay = this.companyRepo.All()
                .Include(x => x.Ratings)
                .Include(x => x.Skills)
                .Include(x => x.Likes)
                .Where(x => x.AddedByUserId.Equals(user.Id))
                .To<T>()
                .FirstOrDefault();

            return compnay;
        }


        public UserInfoOutputModel GetUserInfo(ApplicationUser user)
        {
            return new UserInfoOutputModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
