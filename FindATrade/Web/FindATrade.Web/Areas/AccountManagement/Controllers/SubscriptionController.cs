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

        public IActionResult Add()
        {
            var model = this.subscriptionService.GetPaidOrder<AddSubscriptionModel>();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddSubscriptionModel input)
        {


            return RedirectToAction();
        }
    }
}
