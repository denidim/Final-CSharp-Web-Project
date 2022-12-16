namespace FindATrade.Web.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.AccountManagement;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserAccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountService accountService;
        private readonly ICompanyService companyService;
        private readonly ICompanyServiceService companyServiceService;
        private readonly IRatingService ratingService;
        private readonly IImageService imageService;

        public UserAccountController(
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService,
            IRatingService ratingService,
            IImageService imageService)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
            this.ratingService = ratingService;
            this.imageService = imageService;
        }

        public async Task<IActionResult> GetAccount()
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var accountPage = new UserAccountOutputModel();

                accountPage.UserInfo = this.accountService.GetUserInfo(user);

                accountPage.UserCompany = await this.companyService.GetCompanyByUserIdAsync<CompanyOutputModel>(user.Id);

                if (accountPage.UserCompany != null)
                {
                    accountPage.UserCompany.OutputImageUrl = await this.imageService.GenerateSingleImageUrlForCompany(accountPage.UserCompany.Id);

                    accountPage.UserCompanyServices = await this.companyServiceService.GetAllByUserIdOrCompanyId(user.Id);

                    accountPage.OverallRating = this.ratingService.GetOverallRating(accountPage.UserCompany.Id);
                }

                return this.View(accountPage);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }
    }
}
