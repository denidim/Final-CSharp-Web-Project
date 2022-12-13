namespace FindATrade.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class EmployeeTime : BaseDeletableModel<int>
    {
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [Required]
        public DateTime LogoutTime { get; set; }

        [StringLength(EmployeeConstants.EmployeeTimeCommentMax, MinimumLength = EmployeeConstants.EmployeeTimeCommentMin, ErrorMessage = EmployeeConstants.EmployeeTimeCommentMessage)]
        public string Comment { get; set; }
    }
}