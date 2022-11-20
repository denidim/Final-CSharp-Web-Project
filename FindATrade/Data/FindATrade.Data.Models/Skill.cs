namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Common.Models;

    public class Skill : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
