namespace FindATrade.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly ICompanyService companyService;

        public HomeController(
            IGetCountsService getCountsService,
            ICompanyService companyService)
        {
            this.getCountsService = getCountsService;
            this.companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = this.getCountsService.GetCounts();

            viewModel.PopularCompanies = await this.companyService.GetPopular();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
