namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Skill : BaseDeletableModel<int>
    {
        [StringLength(SkillConstants.NameMax, MinimumLength = SkillConstants.NameMin, ErrorMessage = SkillConstants.NameMessage)]
        public string Name { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
