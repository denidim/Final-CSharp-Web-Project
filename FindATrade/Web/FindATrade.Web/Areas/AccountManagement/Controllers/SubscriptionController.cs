namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Services.Data;
    using Hangfire;
    using Microsoft.AspNetCore.Mvc;

    [Area("AccountManagement")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IBackgroundJobClient backgroundJobClient;

        public SubscriptionController(ISubscriptionService subscriptionService, IBackgroundJobClient backgroundJobClient)
        {
            this.subscriptionService = subscriptionService;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task<IActionResult> Add(int serviceId)
        {
            await this.subscriptionService.AddSubscriptionAsync(serviceId);

            this.backgroundJobClient
                .Schedule<ISubscriptionService>(
                sunscriptionService => sunscriptionService
                .RemoveExpiredSubscriptionsAsync(serviceId),
                TimeSpan.FromMinutes(PaidOrderConstants.TimeSchedule));

            return this.RedirectToAction("GetSingle", "CompanyService", new { id = serviceId, area = " " });
        }
    }
}
