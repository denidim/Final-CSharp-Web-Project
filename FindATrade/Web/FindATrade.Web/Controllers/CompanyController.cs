namespace FindATrade.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

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
            foreach (var item in input.Skills)
            {
                if (item.Name.Length > 30 || item.Name.Length < 4)
                {
                    this.ModelState.AddModelError(string.Empty, "Skill length must be between 4 and 30 characters");

                    return this.View(input);
                }
            }

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
            foreach (var item in input.Skills)
            {
                if (item.Name.Length > 30 || item.Name.Length < 4)
                {
                    this.ModelState.AddModelError(string.Empty, "Skill length must be between 4 and 30 characters");

                    return this.View(input);
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.companyService.UpdateAsync(id, input);

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public IActionResult Delete(int id)
        {
            this.companyService.Delete(id);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetById(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            SingleCompanyModel company = new SingleCompanyModel();

            company.UserCompany = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(id);

            company.UserCompany.OutputImageUrl = await this.imageService.GenerateSingleImageUrlForCompany(id);

            if (company.UserCompany.OutputImageUrl == null)
            {
                company.UserCompany.OutputImageUrl = "https://images.pexels.com/photos/617278/pexels-photo-617278.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";
            }

            company.OverallRating = this.ratingService.GetOverallRating(company.UserCompany.Id);

            company.UserCompanyServices = await this.companyServiceService.GetAllByUserIdOrCompanyId(company.UserCompany.Id);

            company.IsOwner = this.companyService.IsUsersCompany(userId);

            return this.View(company);
        }
    }
}
