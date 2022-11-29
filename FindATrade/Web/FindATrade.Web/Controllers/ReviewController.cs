namespace FindATrade.Web.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : Controller
    {
        private readonly ICompanyService companyService;

        public ReviewController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public IActionResult Write()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Write(ReviewModel model, int id)
        {
            await this.companyService.CreateReview(model, id);

            return this.RedirectToAction("GetById", "Company", new { id = id });
        }
    }
}
