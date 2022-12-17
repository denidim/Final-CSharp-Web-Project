namespace FindATrade.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Common;
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

        public async Task<T> GetPaidOrderAsync<T>(int id)
        {
            var paidOrder = await this.paidOrderRepo.All()
                .Include(x => x.Service)
                .Where(x => x.Service.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return paidOrder;
        }

        public async Task AddSubscriptionAsync(int serviceId)
        {
            var service = this.serviceRepo.All()
                .FirstOrDefault(x => x.Id == serviceId);

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service) + "not found");
            }

            var paidOrder = new PaidOrder
            {
                Name = PaidOrderConstants.Name,
                Price = PaidOrderConstants.Price,
                Terms = PaidOrderConstants.Terms,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMinutes(PaidOrderConstants.TimeSchedule),
            };

            service.PaidOrder = paidOrder;

            await this.serviceRepo.SaveChangesAsync();
        }

        public async Task RemoveExpiredSubscriptionsAsync(int serviceId)
        {
            var service = await this.serviceRepo.All()
                .Include(x => x.PaidOrder)
                .FirstOrDefaultAsync(x => x.Id == serviceId);

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service) + "not found");
            }

            this.paidOrderRepo.HardDelete(service.PaidOrder);
            await this.serviceRepo.SaveChangesAsync();
        }
    }
}
