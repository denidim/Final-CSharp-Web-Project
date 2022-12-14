namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Subscription;
    using Microsoft.AspNetCore.Mvc;

    [Area("AccountManagement")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> Add(int serviceId)
        {
            await this.subscriptionService.AddSubscriptionAsync(serviceId);

            return this.RedirectToAction("GetSingle", "CompanyService", new { id = serviceId, area = " " });
        }
    }
}
