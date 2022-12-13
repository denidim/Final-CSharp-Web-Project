namespace FindATrade.Services.Data
{
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.AccountManagement;
    using FindATrade.Web.ViewModels.Company;

    public interface IAccountService
    {
        UserInfoOutputModel GetUserInfo(ApplicationUser user);

        CompanyOutputModel GetCompanyInfo(ApplicationUser user);

        T GetCompanyInfoByUser<T>(ApplicationUser user);
    }
}
