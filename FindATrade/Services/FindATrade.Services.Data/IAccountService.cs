namespace FindATrade.Services.Data
{
    using System.Collections.Generic;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.UserAccount;

    public interface IAccountService
    {
        UserInfoOutputModel GetUserInfo(ApplicationUser user);

        T GetCompanyInfo<T>(ApplicationUser user);

        IEnumerable<CompanyServiceOutputModel> GetUserCompanyService(ApplicationUser user);
    }
}
