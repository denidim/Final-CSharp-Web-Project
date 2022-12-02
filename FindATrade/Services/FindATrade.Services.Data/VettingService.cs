namespace FindATrade.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class VettingService : IVettingService
    {
        private readonly IDeletableEntityRepository<Vetting> vettingRepo;

        public VettingService(
            IDeletableEntityRepository<Vetting> vettingRepo)
        {
            this.vettingRepo = vettingRepo;
        }

        public async Task<T> GetByServiceIdAsync<T>(int id)
        {
            return await this.vettingRepo.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }
    }
}
