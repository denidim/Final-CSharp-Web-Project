namespace FindATrade.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : BaseController
    {
        private readonly IRatingService ratingService;

        public ReviewController(
            IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        public IActionResult Write()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Write(ReviewModel model, int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                await this.ratingService.CreateReviewAsync(model, id, userId);

                return this.RedirectToAction("GetById", "Company", new { id = id });
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }
    }
}
