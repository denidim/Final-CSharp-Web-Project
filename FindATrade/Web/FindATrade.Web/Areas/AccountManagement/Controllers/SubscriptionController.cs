namespace FindATrade.Web.Areas.AccountManagement.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Common;
    using FindATrade.Services.Data;
    using FindATrade.Web.Controllers;
    using Hangfire;
    using Microsoft.AspNetCore.Mvc;

    [Area("AccountManagement")]
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly ICompanyServiceService companyServiceService;

        public SubscriptionController(
            ISubscriptionService subscriptionService, 
            IBackgroundJobClient backgroundJobClient,
            ICompanyServiceService companyServiceService)
        {
            this.subscriptionService = subscriptionService;
            this.backgroundJobClient = backgroundJobClient;
            this.companyServiceService = companyServiceService;
        }

        public async Task<IActionResult> Add(int serviceId)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool isUsersService = this.companyServiceService.IsUsersService(userId, serviceId);

                if (!isUsersService)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                await this.subscriptionService.AddSubscriptionAsync(serviceId);

                this.backgroundJobClient
                    .Schedule<ISubscriptionService>(
                    sunscriptionService => sunscriptionService
                    .RemoveExpiredSubscriptionsAsync(serviceId),
                    TimeSpan.FromMinutes(PaidOrderConstants.TimeSchedule));

                return this.RedirectToAction("GetSingle", "CompanyService", new { id = serviceId, area = " " });
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }
    }
}
