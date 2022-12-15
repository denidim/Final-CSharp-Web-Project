namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class CompanyRatingsModel : OverallCompanyRating, IMapFrom<Rating>
    {
        public string AddedByUserId { get; set; }

        public string Description { get; set; }
    }
}
