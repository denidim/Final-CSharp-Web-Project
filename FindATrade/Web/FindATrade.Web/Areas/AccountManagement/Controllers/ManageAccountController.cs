using Microsoft.AspNetCore.Mvc;

namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
    [Area("AccountManagement")]
    public class ManageAccountController : Controller
    {
        public IActionResult Manage()
        {
            // TODO: Get Account Service
            // TODO: Return Account For Edit
            return this.View();
        }

        // TODO Edit Account Action
    }
}
