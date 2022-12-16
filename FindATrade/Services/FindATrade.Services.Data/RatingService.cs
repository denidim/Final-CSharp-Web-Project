namespace FindATrade.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FindATrade.Data.Common.Repositories;
    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;

    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Rating> ratingRepo;

        public RatingService(IDeletableEntityRepository<Rating> ratingRepo)
        {
            this.ratingRepo = ratingRepo;
        }

        public async Task CreateReviewAsync(ReviewModel model, int companyId, string userId)
        {
            var rating = this.ratingRepo.All()
                .FirstOrDefault(x => x.CompanyId == companyId && x.AddedByUserId == userId);

            if (rating == null)
            {
                rating = new Rating()
                {
                    AddedByUserId = userId,
                    CompanyId = companyId,
                    Courtesy = model.Courtesy,
                    Tidiness = model.Tidiness,
                    Description = model.Description,
                    Reliability = model.Reliability,
                    Workmanship = model.Workmanship,
                    QuoteAccuracy = model.QuoteAccuracy,
                };

                await this.ratingRepo.AddAsync(rating);
            }

            await this.ratingRepo.SaveChangesAsync();
        }

        public OverallCompanyRating GetOverallRating(int companyId)
        {
            var ratings = this.ratingRepo.All()
                .Where(x => x.CompanyId == companyId)
                .ToList();

            if (!ratings.Any())
            {
                return null;
            }

            return new OverallCompanyRating()
            {
                Tidiness = (int)ratings.Average(x => x.Tidiness),
                Courtesy = (int)ratings.Average(x => x.Courtesy),
                QuoteAccuracy = (int)ratings.Average(x => x.QuoteAccuracy),
                Workmanship = (int)ratings.Average(x => x.Workmanship),
                Reliability = (int)ratings.Average(x => x.Reliability),
            };
        }
    }
}
