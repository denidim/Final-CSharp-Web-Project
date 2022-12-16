namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Data;
    using FindATrade.Web.Controllers;
    using FindATrade.Web.ViewModels.AccountManagement;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("AccountManagement")]
    public class ManageAccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IDeletableEntityRepository<Service> serviceRepo;
        private readonly ICompanyServiceService companyServiceService;

        public ManageAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IDeletableEntityRepository<Service> serviceRepo,
            ICompanyServiceService companyServiceService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.serviceRepo = serviceRepo;
            this.companyServiceService = companyServiceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    EmailConfirmed = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                };

                var result = await this.userManager.CreateAsync(user, model.Password);

                //await this.userManager
                //        .AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypeConstants.FirstName, user.FirstName ?? user.Email));

                await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction("GetAccount", "UserAccount", new { area = " " });
                }

                foreach (var item in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, item.Description);
                }

                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var user = await this.userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (model.ReturnUrl != null)
                        {
                            return this.Redirect(model.ReturnUrl);
                        }

                        return this.RedirectToAction("GetAccount", "UserAccount", new { area = " " });
                    }
                }

                this.ModelState.AddModelError(string.Empty, "Invalid login");
                return this.View(model);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Vett()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Vett(int id, VettModel input)
        {
            try
            {
                var services = await this.serviceRepo
                .All()
                .Include(x => x.Vetting)
                .FirstAsync(x => x.Id == id);

                if (input.IsPassed == true)
                {
                    services.Vetting.ApprovalDate = System.DateTime.UtcNow;
                    services.Vetting.Passed = true;
                    services.Vetting.Description = VettingConstants.Passed;
                }
                else
                {
                    services.Vetting.Description = input.Description;
                }

                await this.serviceRepo.SaveChangesAsync();

                return this.RedirectToAction("GetSingle", "CompanyService", new { id = id, area = " " });
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllForVetting()
        {
            try
            {
                var ids = this.companyServiceService.GetAllForVettingIds();

                return this.View(ids);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Manager));
        //    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Supervisor));
        //    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrator));

        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<IActionResult> AddUsersToRoles()
        //{
        //    var user1 = await userManager.FindByEmailAsync("pesho@abv.bg");
        //    var user2 = await userManager.FindByEmailAsync("fs900220@gmail.com");

        //    await userManager.AddToRoleAsync(user1, RoleConstants.Manager);
        //    await userManager.AddToRolesAsync(user2, new string[] { RoleConstants.Manager, RoleConstants.Supervisor });

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
