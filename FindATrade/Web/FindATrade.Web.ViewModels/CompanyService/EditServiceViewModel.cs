namespace FindATrade.Web.ViewModels.CompanyService
{
    using AutoMapper;
    using FindATrade.Services.Mapping;

    public class EditServiceViewModel : CreateCompanyServiceInputModel, IMapFrom<Data.Models.Service>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Service, EditServiceViewModel>()
                .ForMember(x => x.Images, opt => opt.Ignore())
                .ForMember(x => x.Categories, opt => opt.Ignore());

        }
    }
}
