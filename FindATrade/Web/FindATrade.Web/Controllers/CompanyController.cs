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
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(input);
                }

                ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

                await this.companyService.CreateAsync(input, currentUser);

                return this.RedirectToAction("GetAccount", "UserAccount");
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool isUserComapny = this.CheckIfCompanyBelongsToCurrentUser(id);

                if (!isUserComapny)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                var model = await this.companyService.GetForEditCompanyByIdAsync<EditCompanyViewModel>(id);

                if (model == null)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCompanyViewModel input)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(input);
                }

                await this.companyService.UpdateAsync(id, input);

                return this.RedirectToAction("GetAccount", "UserAccount");
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isUserComapny = this.CheckIfCompanyBelongsToCurrentUser(id);

                if (!isUserComapny)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                await this.companyService.DeleteAsync(id);

                return this.RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetByServiceId(int id)
        {
            try
            {
                var companyId = await this.companyService.GetCompanyByServiceId(id);

                if (companyId == null)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.RedirectToAction(nameof(this.GetById), new { id = companyId });
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                SingleCompanyModel company = new SingleCompanyModel();

                company.UserCompany = await this.companyService.GetCompanyByIdAsync<CompanyOutputModel>(id);

                if (company.UserCompany == null)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                company.UserCompany.OutputImageUrl = await this.imageService
                    .GenerateSingleImageUrlForCompanyAsync(id);

                company.OverallRating = await this.ratingService.GetOverallRating(company.UserCompany.Id);

                company.UserCompanyServices = await this.companyServiceService
                    .GetAllByUserIdOrCompanyId(company.UserCompany.Id);

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                company.IsOwner = this.companyService.IsUsersCompany(userId, company.UserCompany.Id);

                if (company.UserCompanyServices == null ||
                    company.UserCompanyServices.All(x => x.Vetting.Passed == false))
                {
                    if (company.IsOwner)
                    {
                        return this.View(company);
                    }

                    return this.RedirectToAction("Error", "Home");
                }
                else
                {
                    return this.View(company);
                }
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int id = 1)
        {
            var viewModel = new AllCompaniesViewModel
            {
                AllCompanies = await this.companyService.GetAll(id, PagingConstants.ItemsPerPage),
                PageNumber = id,
                EntitiesCount = this.companyService.GetCount(),
                ItemsPerPage = PagingConstants.ItemsPerPage,
            };

            return this.View(viewModel);
        }

        private bool CheckIfCompanyBelongsToCurrentUser(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isUserComapny = this.companyService.IsUsersCompany(userId, id);
            return isUserComapny;
        }
    }
}
