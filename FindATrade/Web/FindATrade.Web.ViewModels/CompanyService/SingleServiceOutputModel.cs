namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;

    using AutoMapper;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class SingleServiceOutputModel : IMapFrom<Service>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsPremium { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public VettingOutputModel Vetting { get; set; }

        public ICollection<PackageModel> Packages { get; set; }

        public ICollection<IFormFile> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Service, SingleServiceOutputModel>()
                .ForMember(x => x.Images, opt => opt.Ignore())
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
