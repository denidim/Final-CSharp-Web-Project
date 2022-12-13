namespace FindATrade.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Vetting : BaseDeletableModel<int>
    {
        [Required]
        public DateTime StartVetting { get; set; }

        [Required]
        public DateTime ApprovalDate { get; set; }

        public bool Passed { get; set; }

        [StringLength(VettingConstants.DescriptionMax, MinimumLength = VettingConstants.DescriptionMin, ErrorMessage = VettingConstants.DescriptionMessage)]
        public string Description { get; set; }

        public int? VettetByEmployeeId { get; set; }

        public Employee VettetByEmployee { get; set; }

        public Service Service { get; set; }
    }
}
