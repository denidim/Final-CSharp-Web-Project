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
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Rating> ratingRepo;

        public RatingService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Rating> ratingRepo)
        {
            this.companyRepo = companyRepo;
            this.ratingRepo = ratingRepo;
        }

        public async Task CreateReview(ReviewModel model, int companyId)
        {
            var company = await this.companyRepo.All()
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == companyId);

            Rating rating = new Rating()
            {
                Courtesy = model.Courtesy,
                Tidiness = model.Tidiness,
                Description = model.Description,
                Reliability = model.Reliability,
                Workmanship = model.Workmanship,
                QuoteAccuracy = model.QuoteAccuracy,
            };

            company.Ratings.Add(rating);
            await this.companyRepo.SaveChangesAsync();
        }

        public OverallCompanyRating GetOverallRating(int companyId)
        {
            var ratings = this.ratingRepo.All()
                .Where(x => x.CompanyId == companyId);

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
