namespace FindATrade.Web.Controllers
{
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.Subscription;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CompanyServiceController : BaseController
    {
        private readonly ICompanyServiceService companyServiceService;
        private readonly IImageService imageService;
        private readonly ISubscriptionService subscriptionService;
        private readonly ICompanyService companyService;

        public CompanyServiceController(
            ICompanyServiceService companyServiceService,
            IImageService imageService,
            ISubscriptionService subscriptionService,
            ICompanyService companyService)
        {
            this.companyServiceService = companyServiceService;
            this.imageService = imageService;
            this.subscriptionService = subscriptionService;
            this.companyService = companyService;
        }

        public async Task<IActionResult> Create(int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool isUserComapny = this.companyService.IsUsersCompany(userId, id);

                if (!isUserComapny)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                var model = new CreateCompanyServiceInputModel
                {
                    Categories = await this.companyServiceService.GetGategoriesAsync(),
                };

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyServiceInputModel input, int id)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    input.Categories = await this.companyServiceService.GetGategoriesAsync();

                    return this.View(input);
                }

                await this.companyServiceService.CreateAsync(input, id);

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
                bool isUsersService = this.CheckIfServiceBelongToCurrentUser(id);

                var model = await this.companyServiceService.GetByIdAsync<EditServiceViewModel>(id);

                if (model == null || !isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                model.Categories = await this.companyServiceService.GetGategoriesAsync();

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditServiceViewModel input)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    input.Categories = await this.companyServiceService.GetGategoriesAsync();

                    return this.View(input);
                }

                await this.companyServiceService.UpdateAsync(id, input);

                return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> EditImage(int id)
        {
            try
            {
                bool isUsersService = this.CheckIfServiceBelongToCurrentUser(id);

                if (!isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                var model = await this.imageService.GetAllPictures(id);

                if (model == null || !model.Any())
                {
                    return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
                }

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeletePicture(int id, string name)
        {
            try
            {
                bool isUsersService = this.CheckIfServiceBelongToCurrentUser(id);

                if (!isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                await this.imageService.CloudDelete(name);

                return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
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
                bool isUsersService = this.CheckIfServiceBelongToCurrentUser(id);

                if (!isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                await this.companyServiceService.DeleteAsync(id);

                return this.RedirectToAction("GetAccount", "UserAccount");
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public IActionResult AddImages()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImages(AddImages input, int id)
        {
            try
            {
                bool isUsersService = this.CheckIfServiceBelongToCurrentUser(id);

                if (!isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                if (input.Images == null)
                {
                    return this.RedirectToAction(nameof(this.AddImages), new { id = id });
                }

                await this.imageService.Add(input, id);

                return this.RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var model = await this.companyServiceService.GetByIdAsync<SingleServiceOutputModel>(id);

                if (model == null)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                model.Images = await this.imageService.GenerateImageUrlsForService(model.Id);

                model.Subscription = await this.subscriptionService.GetPaidOrderAsync<SubscriptionModel>(model.Id);

                model.CompanyServicesByCategory = await this.companyServiceService.GetAllByCategory(model.CategoryName);

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                model.IsOwner = this.companyServiceService.IsUsersService(userId, model.Id);

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        private bool CheckIfServiceBelongToCurrentUser(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isUsersService = this.companyServiceService.IsUsersService(userId, id);

            return isUsersService;
        }
    }
}
