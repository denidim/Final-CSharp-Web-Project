namespace FindATrade.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class CompanyController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;
        private readonly ICompanyServiceService companyServiceService;
        private readonly IRatingService ratingService;
        private readonly IImageService imageService;

        public CompanyController(
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService,
            IRatingService ratingService,
            IImageService imageService)
        {
            this.userManager = userManager;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
            this.ratingService = ratingService;
            this.imageService = imageService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.companyService.CreateAsync(input, currentUser);
            }
            catch (System.Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.companyService.GetCompanyByIdAsync<EditCompanyViewModel>(id);

            // TODO Get By Id Service
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCompanyViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.companyService.UpdateAsync(id, input);

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.companyService.DeleteAsync(id);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetByServiceId(int id)
        {
            var companyId = await this.companyService.GetCompanyByServiceId(id);

            return this.RedirectToAction(nameof(this.GetById), new { id = companyId });
        }

        public async Task<IActionResult> GetById(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            SingleCompanyModel company = new SingleCompanyModel();

            company.UserCompany = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(id);

            company.UserCompany.OutputImageUrl = await this.imageService.GenerateSingleImageUrlForCompany(id);

            company.OverallRating = this.ratingService.GetOverallRating(company.UserCompany.Id);

            company.UserCompanyServices = await this.companyServiceService.GetAllByUserIdOrCompanyId(company.UserCompany.Id);

            company.IsOwner = this.companyService.IsUsersCompany(userId);

            return this.View(company);
        }
    }
}
