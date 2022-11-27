namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class CompanyRatingsModel : OverallCompanyRating, IMapFrom<Rating>
    {
        public string Description { get; set; }
    }
}
