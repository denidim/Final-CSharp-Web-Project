namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;

    using FindATrade.Web.ViewModels.CompanyService;

    public class SingleCompanyModel
    {
        public CompanyOutputModel UserCompany { get; set; }

        public IEnumerable<CompanyServiceOutputModel> UserCompanyServices { get; set; }
    }
}
