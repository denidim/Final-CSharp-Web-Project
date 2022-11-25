﻿namespace FindATrade.Web.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;

        public CompanyController(
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService)
        {
            this.userManager = userManager;
            this.companyService = companyService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyInputModel input)
        {
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

            this.TempData["Message"] = "Recipe added successfully";

            // TODO: Redirect to correct page
            return this.Redirect("/");
        }
    }
}