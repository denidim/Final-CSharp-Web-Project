namespace FindATrade.Web.ViewModels.Home
{
    using FindATrade.Services.Mapping;

    public class IndexPageViewModel : IMapFrom<Data.Models.Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
