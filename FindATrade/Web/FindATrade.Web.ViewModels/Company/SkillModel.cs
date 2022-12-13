namespace FindATrade.Web.ViewModels.Company
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SkillModel : IMapFrom<Skill>
    {
        [Display(Name = "Skill Name (optional)")]
        [StringLength(SkillConstants.NameMax, MinimumLength = SkillConstants.NameMin, ErrorMessage = SkillConstants.NameMessage)]
        public string Name { get; set; }
    }
}
