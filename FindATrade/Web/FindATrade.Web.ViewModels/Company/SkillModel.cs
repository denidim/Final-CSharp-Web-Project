namespace FindATrade.Web.ViewModels.Company
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SkillModel : IMapFrom<Skill>
    {
        [Display(Name = "Skill Name (optional)")]
        [StringLength(30, MinimumLength =3, ErrorMessage = "Skill Name must be between {2} and {1} characters.")]
        public string Name { get; set; }
    }
}
