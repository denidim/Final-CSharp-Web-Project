namespace FindATrade.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Common.Models;

    public class EmployeeTime : BaseDeletableModel<int>
    {
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [Required]
        public DateTime LogoutTime { get; set; }

        public string Comment { get; set; }
    }
}