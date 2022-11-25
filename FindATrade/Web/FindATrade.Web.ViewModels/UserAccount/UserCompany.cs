namespace FindATrade.Web.ViewModels.UserAccount
{
    using System.Collections.Generic;

    using AutoMapper;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class UserCompany : IMapFrom<Company>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int Likes { get; set; }

        public IEnumerable<SkillOutputModel> Skills { get; set; }

        public ICollection<CompanyRatings> Ratings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, UserCompany>()
                .ForMember(x => x.Address, options =>
                options.MapFrom(x => $"{x.Address.Street} {x.Address.City}"))
                .ForMember(x => x.Likes, options =>
                options.MapFrom(x => x.Likes.Count));
        }
    }
}