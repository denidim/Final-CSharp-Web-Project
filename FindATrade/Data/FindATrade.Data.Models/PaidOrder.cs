namespace FindATrade.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Data.Common.Models;

    public class PaidOrder : BaseDeletableModel<int>
    {
        public PaidOrder()
        {
            this.Services = new HashSet<Service>();
        }

        public PaidOrderPackageType PaidOrderPackageType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
