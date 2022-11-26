namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Services.Mapping;
    using System.Collections.Generic;

    public class CompanyOutputModel : IMapFrom<FindATrade.Data.Models.Company>
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
    }
}
