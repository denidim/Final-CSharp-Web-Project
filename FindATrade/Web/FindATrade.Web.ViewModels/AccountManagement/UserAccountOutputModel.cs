namespace FindATrade.Web.ViewModels.AccountManagement
{
    using System.Collections.Generic;

    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;

    public class UserAccountOutputModel
    {
        public UserInfoOutputModel UserInfo { get; set; }

        public CompanyOutputModel UserCompany { get; set; }

        public OverallCompanyRating OverallRating { get; set; }

        public IEnumerable<CompanyServiceOutputModel> UserCompanyServices { get; set; }
    }
}
