namespace FindATrade.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;


    using FindATrade.Data.Common.Repositories;
    using FindATrade.Common;
    using FindATrade.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class LikeService : ILikeService
    {
        private readonly IDeletableEntityRepository<Like> likeRepo;

        public LikeService(IDeletableEntityRepository<Like> likeRepo)
        {
            this.likeRepo = likeRepo;
        }

        public async Task<int> GetLikeCountAsync(int companyId)
        {
            var likes = await this.likeRepo.All()
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();

            if (likes == null || likes.Count < 1)
            {
                throw new ArgumentNullException(Exceptions.LikeExMessage);
            }

            return likes.Count();
        }

        public async Task SetLike(int companyId, string userId)
        {
            var like = await this.likeRepo.All()
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.AddedByUserId == userId);

            if (like == null)
            {
                like = new Like
                {
                    CompanyId = companyId,
                    AddedByUserId = userId,
                };

                await this.likeRepo.AddAsync(like);
            }

            await this.likeRepo.SaveChangesAsync();
        }
    }
}
