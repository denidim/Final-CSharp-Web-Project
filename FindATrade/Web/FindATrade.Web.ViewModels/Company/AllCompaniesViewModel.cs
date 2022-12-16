using FindATrade.Web.ViewModels.Home;
using System.Collections.Generic;

namespace FindATrade.Web.ViewModels.Company
{
    public class AllCompaniesViewModel
    {
        public int PageNumber { get; set; }

        public IEnumerable<IndexPageOutputViewModel> AllCompanies { get; set; }
    }
}
