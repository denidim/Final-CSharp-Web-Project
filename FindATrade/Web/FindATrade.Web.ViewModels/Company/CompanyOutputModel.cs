namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FindATrade.Services.Mapping;

    public class CompanyOutputModel : IMapFrom<Data.Models.Company>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int Likes { get; set; }

        public IEnumerable<SkillModel> Skills { get; set; }

        public ICollection<CompanyRatingsModel> Ratings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Company, CompanyOutputModel>()
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => $"{x.Address.Street} - {x.Address.City}"))

                .ForMember(x => x.Likes, opt =>
                    opt.MapFrom(x => x.Likes.Count));
        }
    }
}
