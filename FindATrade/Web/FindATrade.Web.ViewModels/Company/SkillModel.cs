namespace FindATrade.Web.ViewModels.Company
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SkillModel : IMapFrom<Skill>
    {
        [StringLength(30)]
        public string Name { get; set; }
    }
}
