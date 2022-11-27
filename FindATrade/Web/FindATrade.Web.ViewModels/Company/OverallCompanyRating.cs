namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class OverallCompanyRating
    {
        public int Tidiness { get; set; }

        public int Workmanship { get; set; }

        public int Reliability { get; set; }

        public int Courtesy { get; set; }

        public int QuoteAccuracy { get; set; }

    }
}
