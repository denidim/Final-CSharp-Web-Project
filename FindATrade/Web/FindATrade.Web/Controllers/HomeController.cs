namespace FindATrade.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels;
    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly ICompanyService companyService;
        private readonly ISubscriptionService subscriptionService;
        private readonly IBackgroundJobClient backgroundJobClient;

        public HomeController(
            IGetCountsService getCountsService,
            ICompanyService companyService,
            ISubscriptionService subscriptionService,
            IBackgroundJobClient backgroundJobClient)
        {
            this.getCountsService = getCountsService;
            this.companyService = companyService;
            this.subscriptionService = subscriptionService;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = this.getCountsService.GetCounts();

                viewModel.PopularCompanies = await this.companyService.GetPopular();

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
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
