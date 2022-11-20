namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Common.Models;

    public class Employee : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }

        public EmployeeTime EmployeeTime { get; set; }
    }
}