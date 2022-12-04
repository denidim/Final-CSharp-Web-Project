namespace FindATrade.Web.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.UserAccount;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountService accountService;
        private readonly ICompanyService companyService;
        private readonly ICompanyServiceService companyServiceService;
        private readonly IRatingService ratingService;

        public UserAccountController(
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService,
            IRatingService ratingService)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
            this.ratingService = ratingService;
        }

        public async Task<IActionResult> GetAccount()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var accountPage = new UserAccountOutputModel()
            {
                UserInfo = this.accountService.GetUserInfo(user),
                UserCompany = await this.companyService.GetCompanyByUserIdAsync<CompanyOutputModel>(user.Id),
                UserCompanyServices = await this.companyServiceService.GetAllCompanyServices(user.Id),
            };

            if (accountPage.UserCompany != null)
            {
                accountPage.OverallRating = this.ratingService.GetOverallRating(accountPage.UserCompany.Id);
            }

            this.ViewBag.Title = "My Account";
            this.ViewBag.Message = "This is how customers see your acount";

            return this.View(accountPage);
        }
    }
}
