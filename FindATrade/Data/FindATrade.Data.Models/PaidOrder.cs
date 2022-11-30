namespace FindATrade.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Common.Models;

    public class PaidOrder : BaseDeletableModel<int>
    {

        public PaidOrderPackageType PaidOrderPackageType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public Service Service { get; set; }
    }
}
