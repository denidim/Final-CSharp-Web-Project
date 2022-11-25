namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.UserAccount;
    using Microsoft.EntityFrameworkCore;

    public class AccountService : IAccountService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public AccountService(IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public T GetCompanyInfo<T>(ApplicationUser user)
        {
            var company = this.companyRepo
                .All()
                .Where(x => x.AddedByUserId == user.Id)
                .To<T>()
                .FirstOrDefault();

            return company;
        }

        public IEnumerable<CompanyServiceOutputModel> GetUserCompanyService(ApplicationUser user)
        {
            var userCompanyService = this.companyRepo
                .All()
                .FirstOrDefault(x => x.AddedByUserId == user.Id)
                .Services;

            var companyService = new List<CompanyServiceOutputModel>();

            foreach (var item in userCompanyService)
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
                    PaidOrder = new PaidOrderOutputModel()
                    {
                        StartDate = item.PaidOrder.StartDate.ToString(),
                        EndDate = item.PaidOrder.EndDate.ToString(),
                        Name = item.PaidOrder.PaidOrderPackageType.Name,
                        Price = item.PaidOrder.PaidOrderPackageType.Price.ToString(),
                        Terms = item.PaidOrder.PaidOrderPackageType.Terms,
                    },
                    Vetting = new VettingOutputModel()
                    {
                        Passed = item.Vetting.Passed,
                        Description = item.Vetting.Description,
                    },
                    Packages = package,
                };

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
