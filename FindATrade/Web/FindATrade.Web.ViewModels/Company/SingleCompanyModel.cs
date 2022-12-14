namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;

    using FindATrade.Web.ViewModels.CompanyService;

    public class SingleCompanyModel
    {
        public bool IsOwner { get; set; }

        public CompanyOutputModel UserCompany { get; set; }

        public OverallCompanyRating OverallRating { get; set; }

        public IEnumerable<SingleServiceOutputModel> UserCompanyServices { get; set; }
    }
}
