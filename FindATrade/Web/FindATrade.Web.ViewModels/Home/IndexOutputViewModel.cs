namespace FindATrade.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexOutputViewModel
    {
        public int CategoriesCount { get; set; }

        public int CompaniesCount { get; set; }

        public int ServicesCount { get; set; }

        public IEnumerable<IndexPageOutputViewModel> PopularCompanies { get; set; }
    }
}
