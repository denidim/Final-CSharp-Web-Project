namespace FindATrade.Web.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : Controller
    {
        private readonly IRatingService ratingService;

        public ReviewController(IRatingService ratingService)
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
            await this.ratingService.CreateReview(model, id);

            return this.RedirectToAction("GetById", "Company", new { id = id });
        }
    }
}
