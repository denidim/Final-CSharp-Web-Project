namespace FindATrade.Web.Areas.Administration.Controllers
{
    using FindATrade.Common;
    using FindATrade.Services.Data;
    using FindATrade.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
