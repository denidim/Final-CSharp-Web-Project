namespace FindATrade.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [AllowAnonymous]
    public class CompanyController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;
        private readonly ICompanyServiceService companyServiceService;
        private readonly IRatingService ratingService;

        public CompanyController(
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService,
            IRatingService ratingService)
        {
            this.userManager = userManager;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
            this.ratingService = ratingService;
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

            this.TempData["Message"] = "Company added successfully";

            // TODO: Redirect to correct page
            return this.Redirect("/");
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

        public async Task<IActionResult> GetById(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            SingleCompanyModel company = new SingleCompanyModel();

            company.UserCompany = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(id);

            company.UserCompany.OutputImageUrl = await this.companyService.GenerateImageUrl(id);

            company.OverallRating = this.ratingService.GetOverallRating(company.UserCompany.Id);

            company.UserCompanyServices = await this.companyServiceService.GetAllCompanyServices(company.UserCompany.Id);

            if (company.UserCompanyServices.Any())
            {
                company.IsOwner = this.companyServiceService
                    .IsUsersCompany(company.UserCompanyServices.First().Id, userId);
            }
            else
            {
                company.IsOwner = false;
            }

            return this.View(company);
        }
    }
}
