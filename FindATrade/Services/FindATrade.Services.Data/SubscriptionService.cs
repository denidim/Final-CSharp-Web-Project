using FindATrade.Data.Common.Repositories;
using FindATrade.Data.Models;
using FindATrade.Services.Mapping;
using FindATrade.Web.ViewModels.Subscription;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FindATrade.Services.Data
{
    public class SubscriptionService
    {
        private readonly IDeletableEntityRepository<PaidOrder> paidOrderRepo;
        private readonly IDeletableEntityRepository<PaidOrderPackageType> paidOrderPackageTypeRepo;

        public SubscriptionService(
            IDeletableEntityRepository<PaidOrder> paidOrderRepo,
            IDeletableEntityRepository<PaidOrderPackageType> paidOrderPackageTypeRepo)
        {
            this.paidOrderRepo = paidOrderRepo;
            this.paidOrderPackageTypeRepo = paidOrderPackageTypeRepo;
        }

        public async Task<T> GetPaidOrder<T>()
        {
            var company = await this.paidOrderPackageTypeRepo.All()
                .To<T>()
                .FirstOrDefaultAsync();

            return company;
        }
    }
}
