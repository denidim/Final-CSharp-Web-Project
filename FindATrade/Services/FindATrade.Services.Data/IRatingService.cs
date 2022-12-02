namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Review;

    public interface IRatingService
    {
        Task CreateReview(ReviewModel model, int companyId);

        OverallCompanyRating GetOverallRating(int companyId);
    }
}
