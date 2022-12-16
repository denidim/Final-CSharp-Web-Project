namespace FindATrade.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly ICompanyService companyService;
        private readonly ISeederServcie seederServcie;
        private readonly IDeletableEntityRepository<Vetting> vettingRepo;

        public HomeController(
            IGetCountsService getCountsService,
            ICompanyService companyService,
            ISeederServcie seederServcie,
            IDeletableEntityRepository<Vetting> vettingRepo)
        {
            this.getCountsService = getCountsService;
            this.companyService = companyService;
            this.seederServcie = seederServcie;
            this.vettingRepo = vettingRepo;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = this.getCountsService.GetCounts();

                viewModel.PopularCompanies = await this.companyService.GetSubscribed();

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Vett()
        {
            var vettings = await this.vettingRepo.All().ToListAsync();

            foreach (var vetting in vettings)
            {
                vetting.Passed = true;
                vetting.Description = "Passed";
            }

            await this.vettingRepo.SaveChangesAsync();

            return this.Redirect("/");
        }

        public async Task<IActionResult> Seed()
        {
            try
            {
                await this.seederServcie.SeedAsync(50);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.Redirect("/");
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
