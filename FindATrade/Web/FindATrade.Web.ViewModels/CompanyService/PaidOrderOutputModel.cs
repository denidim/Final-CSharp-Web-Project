using FindATrade.Data.Models;
using FindATrade.Services.Mapping;

namespace FindATrade.Web.ViewModels.CompanyService
{
    public class PaidOrderOutputModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Terms { get; set; }
    }
}