﻿namespace FindATrade.Services.Data
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
            var service = await this.serviceRepo.All()
                .FirstOrDefaultAsync(x => x.Id == serviceId);

            var paidOrder = new PaidOrder
            {
                Name = PaidOrderConstants.Name,
                Price = PaidOrderConstants.Price,
                Terms = PaidOrderConstants.Terms,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMinutes(PaidOrderConstants.Time),
            };

            service.PaidOrder = paidOrder;

            await this.serviceRepo.SaveChangesAsync();
        }

        public async Task RemoveExpiredSubscriptionsAsync()
        {
            var services = await this.serviceRepo.All()
                .Include(x => x.PaidOrder)
                .Where(x => x.PaidOrder.EndDate < DateTime.UtcNow)
                .ToListAsync();

            foreach (var service in services)
            {
                this.paidOrderRepo.HardDelete(service.PaidOrder);
                await this.serviceRepo.SaveChangesAsync();
            }
        }
    }
}
