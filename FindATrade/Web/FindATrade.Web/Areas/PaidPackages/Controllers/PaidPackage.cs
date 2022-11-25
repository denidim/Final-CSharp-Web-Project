using Microsoft.AspNetCore.Mvc;

namespace FindATrade.Web.Areas.PaidPackages.Controllers
{
    [Area("PaidPackages")]
    public class PaidPackage : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
