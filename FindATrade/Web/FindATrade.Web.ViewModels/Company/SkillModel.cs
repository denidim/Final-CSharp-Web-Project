namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SkillModel : IMapFrom<Skill>
    {
        public string Name { get; set; }
    }
}
