namespace FindATrade.Web.ViewModels.CompanyService
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class VettingOutputModel : IMapFrom<Vetting>
    {
        public bool Passed { get; set; }

        public string Description { get; set; }
    }
}
