namespace FindATrade.Web.ViewModels.Home
{
    using AutoMapper;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class IndexPageViewModel : IMapFrom<Company>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, IndexPageViewModel>()
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description.Substring(0, 60)));
        }
    }
}
