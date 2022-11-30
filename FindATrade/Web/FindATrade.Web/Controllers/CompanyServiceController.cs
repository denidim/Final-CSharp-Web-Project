﻿namespace FindATrade.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyServiceController : Controller
    {
        private readonly ICompanyServiceService companyServiceService;

        public CompanyServiceController(ICompanyServiceService companyServiceService)
        {
            this.companyServiceService = companyServiceService;
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

            this.TempData["Message"] = "Recipe added successfully";

            return this.RedirectToAction("Index", "Home");//return this.RedirectToAction("GetAccount", "UserAccount");
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

            return this.RedirectToAction("GetAccount", "UserAccount");
        }

        public async Task<IActionResult> GetSingle(int id)
        {
            var model = await this.companyServiceService.GetByIdAsync<SingleServiceOutputModel>(id);

            return this.View(model);
        }

    }
}
