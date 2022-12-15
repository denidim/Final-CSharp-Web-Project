namespace FindATrade.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.CompanyService;
    using FindATrade.Web.ViewModels.Subscription;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyServiceController : BaseController
    {
        private readonly ICompanyServiceService companyServiceService;
        private readonly IImageService imageService;
        private readonly ISubscriptionService subscriptionService;

        public CompanyServiceController(
            ICompanyServiceService companyServiceService,
            IImageService imageService,
            ISubscriptionService subscriptionService)
        {
            this.companyServiceService = companyServiceService;
            this.imageService = imageService;
            this.subscriptionService = subscriptionService;
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

            return this.RedirectToAction("GetAccount", "UserAccount");
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

            return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
        }

        public async Task<IActionResult> EditImage(int id)
        {
            var model = await this.imageService.GetAllPictures(id);

            if (model == null || !model.Any())
            {
                return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
            }

            return this.View(model);
        }

        public async Task<IActionResult> DeletePicture(int id, string name)
        {
            await this.imageService.CloudDelete(name);

            await this.imageService.Delete(name);

            return this.RedirectToAction(nameof(this.GetSingle), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.companyServiceService.DeleteAsync(id);

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public IActionResult AddImages()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImages(AddImages input, int id)
        {

            await this.imageService.Add(input, id);

            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetSingle(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await this.companyServiceService.GetByIdAsync<SingleServiceOutputModel>(id);

            model.Images = await this.imageService.GenerateImageUrlsForService(model.Id);

            model.Subscription = await this.subscriptionService.GetPaidOrderAsync<SubscriptionModel>(model.Id);

            model.CompanyServicesByCategory = await this.companyServiceService.GetAllByCategory(model.CategoryName);

            model.IsOwner = this.companyServiceService.IsUsersService(userId);

            return this.View(model);
        }
    }
}
