namespace FindATrade.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int CategoriesCount { get; set; }

        public int CompaniesCount { get; set; }

        public int ServicesCount { get; set; }

        // TODO: add Image
        public IEnumerable<IndexPageViewModel> PopularCompanies { get; set; }

    }
}
