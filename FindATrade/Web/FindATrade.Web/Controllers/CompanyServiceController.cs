namespace FindATrade.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyServiceController : Controller
    {
        private readonly ICompanyServiceService companyServiceService;
        private readonly UserManager<ApplicationUser> userManager;

        public CompanyServiceController(
            ICompanyServiceService companyServiceService,
            UserManager<ApplicationUser> userManager)
        {
            this.companyServiceService = companyServiceService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateCompanyServiceInputModel
            {
                Categories = await this.companyServiceService.GetGategoriesAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyServiceInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.companyServiceService.GetGategoriesAsync();

                return this.View(input);
            }

            try
            {
                await this.companyServiceService.CreateAsync(input, id);
            }
            catch (System.Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            this.TempData["Message"] = "Service added successfully";

            return this.RedirectToAction("Index", "Home");//return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.companyServiceService.GetByIdAsync<EditServiceViewModel>(id);

            model.Categories = await this.companyServiceService.GetGategoriesAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditServiceViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.companyServiceService.GetGategoriesAsync();
                return this.View(input);
            }

            await this.companyServiceService.UpdateAsync(id, input);

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public async Task<IActionResult> GetSingle(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await this.companyServiceService.GetByIdAsync<SingleServiceOutputModel>(id);
            model.CompanyServicesByCategory = await this.companyServiceService.GetAllByCategory<CompanyServiceByCategoryModel>(model.CategoryName);
            model.IsOwner = this.companyServiceService.IsUsersCompany(model.Id, userId);
            return this.View(model);
        }
    }
}
