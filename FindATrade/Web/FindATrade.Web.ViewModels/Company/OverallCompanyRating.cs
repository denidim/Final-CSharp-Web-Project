namespace FindATrade.Web.ViewModels.Company
{
    using System.Linq;

    using AutoMapper;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class OverallCompanyRating : IMapFrom<Company>, IHaveCustomMappings
    {
        public int Tidiness { get; set; }

        public int Workmanship { get; set; }

        public int Reliability { get; set; }

        public int Courtesy { get; set; }

        public int QuoteAccuracy { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Company, OverallCompanyRating>()
          .ForMember(x => x.Tidiness, opt =>
                    opt.MapFrom(x => x.Ratings.Average(x => x.Tidiness)))

                .ForMember(x => x.Workmanship, opt =>
                    opt.MapFrom(x => x.Ratings.Average(x => x.Workmanship)))

                .ForMember(x => x.Reliability, opt =>
                    opt.MapFrom(x => x.Ratings.Average(x => x.Reliability)))

                .ForMember(x => x.Courtesy, opt =>
                    opt.MapFrom(x => x.Ratings.Average(x => x.Courtesy)))

                .ForMember(x => x.QuoteAccuracy, opt =>
                    opt.MapFrom(x => x.Ratings.Average(x => x.QuoteAccuracy)));
        }
    }
}
