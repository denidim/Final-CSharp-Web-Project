namespace FindATrade.Web.ViewModels.Company
{
    using System;
    using System.Collections.Generic;

    using FindATrade.Web.ViewModels.Home;

    public class AllCompaniesViewModel : PagingViewModel
    {
        public IEnumerable<IndexPageOutputViewModel> AllCompanies { get; set; }
    }
}
