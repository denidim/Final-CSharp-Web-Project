using FindATrade.Data.Models;
using FindATrade.Services.Mapping;

namespace FindATrade.Web.ViewModels.CompanyService
{
    public class VettingOutputModel
    {
        public bool Passed { get; set; }

        public string Description { get; set; }
    }
}
