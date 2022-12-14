namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
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

        public IActionResult Add(int serviceId, int id)
        {
            //await this.subscriptionService.AddSubscription(serviceId, id);
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(SubscriptionModel input)
        {


            return RedirectToAction();
        }
    }
}
