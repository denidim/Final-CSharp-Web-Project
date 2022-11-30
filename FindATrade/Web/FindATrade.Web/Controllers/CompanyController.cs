namespace FindATrade.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
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
        private readonly IDeletableEntityRepository<Service> serviceRepo;

        public CompanyController(
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService,
            ICompanyServiceService companyServiceService,
            IDeletableEntityRepository<Service> serviceRepo)
        {
            this.userManager = userManager;
            this.companyService = companyService;
            this.companyServiceService = companyServiceService;
            this.serviceRepo = serviceRepo;
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
            SingleCompanyModel company = new SingleCompanyModel();
            company.UserCompany = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(id);
            company.UserCompanyServices = this.companyServiceService.GetAllCompanyServices(company.UserCompany.Id);
            return this.View(company);
        }
    }
}
