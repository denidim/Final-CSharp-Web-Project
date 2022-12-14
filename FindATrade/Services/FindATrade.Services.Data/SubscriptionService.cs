namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDeletableEntityRepository<PaidOrder> paidOrderRepo;
        private readonly IDeletableEntityRepository<Service> serviceRepo;

        public SubscriptionService(
            IDeletableEntityRepository<PaidOrder> paidOrderRepo,
            IDeletableEntityRepository<Service> serviceRepo)
        {
            this.paidOrderRepo = paidOrderRepo;
            this.serviceRepo = serviceRepo;
        }

        public async Task<T> GetPaidOrder<T>()
        {
            var company = await this.paidOrderRepo.All()
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task AddSubscription(int serviceId, int id)
        {
            var paidOrder = new PaidOrder()
            {
                StartDate = System.DateTime.UtcNow,
                EndDate = System.DateTime.UtcNow.AddDays(30),
            };


            var service = await this.serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Id == serviceId);

            service.PaidOrder = paidOrder;
        }
    }
}
