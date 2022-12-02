namespace FindATrade.Web.ViewModels.CompanyService
{
    using FindATrade.Services.Mapping;

    public class CompanyServiceByCategoryModel : IMapFrom<Data.Models.Service>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
