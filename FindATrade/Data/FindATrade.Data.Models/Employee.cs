namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Employee : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(EmployeeConstants.EmplayeeMax, MinimumLength = EmployeeConstants.EmplayeeMin, ErrorMessage = EmployeeConstants.EmployeeMessage)]
        public string JobTitle { get; set; }

        public EmployeeTime EmployeeTime { get; set; }
    }
}
