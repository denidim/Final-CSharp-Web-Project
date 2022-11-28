namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.UserAccount;
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

        public IEnumerable<CompanyServiceOutputModel> GetUserCompanyService(ApplicationUser user)
        {

            var userCompany = this.companyRepo
                .All()
                .Where(x => x.AddedByUserId == user.Id)
                .Include(x => x.Services)
                .ThenInclude(x => x.Vetting)
                .Include(x => x.Services)
                .ThenInclude(x => x.PaidOrder)
                .Include(x => x.Services)
                .ThenInclude(x => x.Packages)
                .Include(x => x.Services)
                .ThenInclude(x => x.Categotry)
                .Include(x => x.Skills)
                .FirstOrDefault();

            if (userCompany == null)
            {
                return null;
            }

            var companyService = new List<CompanyServiceOutputModel>();

            foreach (var item in userCompany.Services)
            {
                var package = new List<PackageModel>();

                foreach (var packageItem in item.Packages)
                {
                    package.Add(new PackageModel()
                    {
                        Price = packageItem.Price,
                        Description = packageItem.Descrtiption,
                    });
                }

                var service = new CompanyServiceOutputModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsPremium = item.IsPremium,
                    Description = item.Description,
                    CategoryName = item.Categotry.Name,
                    Packages = package,
                };

                if (item.PaidOrder != null)
                {
                    service.PaidOrder = new PaidOrderOutputModel()
                    {
                        StartDate = item.PaidOrder.StartDate.ToString(),
                        EndDate = item.PaidOrder.EndDate.ToString(),
                        Name = item.PaidOrder.PaidOrderPackageType.Name,
                        Price = item.PaidOrder.PaidOrderPackageType.Price.ToString(),
                        Terms = item.PaidOrder.PaidOrderPackageType.Terms,
                    };
                }
                else
                {
                    service.PaidOrder = null;
                }

                if (service.Vetting != null)
                {
                    service.Vetting = new VettingOutputModel()
                    {
                        Passed = item.Vetting.Passed,
                        Description = item.Vetting.Description,
                    };
                }
                else
                {
                    service.Vetting = null;
                }


                companyService.Add(service);
            }

            return companyService;
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
