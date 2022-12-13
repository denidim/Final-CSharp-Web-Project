namespace FindATrade.Web.ViewModels.CompanyService
{
    using FindATrade.Services.Mapping;

    public class CompanyServiceByCategoryOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string OutputImageUrl { get; set; }
    }
}
