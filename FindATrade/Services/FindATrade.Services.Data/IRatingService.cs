namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Review;

    public interface IRatingService
    {
        Task CreateReviewAsync(ReviewModel model, int companyId, string userId);

        OverallCompanyRating GetOverallRating(int companyId);
    }
}
