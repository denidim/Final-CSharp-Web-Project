namespace FindATrade.Web.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.UserAccount;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountService accountService;
        private readonly ICompanyService companyService;
        private readonly ICompanyServiceService companyServiceService;

        public UserAccountController(
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
        }

        public async Task<IActionResult> GetAccount()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var accountPage = new UserAccountOutputModel()
            {
                UserInfo = this.accountService.GetUserInfo(user),
                UserCompany = await this.companyService.GetCompanyByUserIdAsync<CompanyOutputModel>(user.Id),
                UserCompanyServices = this.companyServiceService.GetUserCompanyService(user),
            };

            return this.View(accountPage);
        }

        public IActionResult EditProfile(string id)
        {
            // TODO Get By Id Service
            return this.View();
        }
    }
}
