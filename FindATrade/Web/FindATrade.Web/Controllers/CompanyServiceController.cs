namespace FindATrade.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyServiceController : Controller
    {
        private readonly ICompanyServiceService companyServiceService;

        public CompanyServiceController(ICompanyServiceService companyServiceService)
        {
            this.companyServiceService = companyServiceService;
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
        public async Task<IActionResult> Create(CreateCompanyServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.companyServiceService.GetGategoriesAsync();

                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.companyServiceService.CreateAsync(input, userId);
            }
            catch (System.Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            this.TempData["Message"] = "Recipe added successfully";

            // TODO: Redirect to correct page
            return this.Redirect("/");
        }

        public IActionResult EditService(int id)
        {
            // TODO Get By Id Service
            return this.View();
        }
    }
}
