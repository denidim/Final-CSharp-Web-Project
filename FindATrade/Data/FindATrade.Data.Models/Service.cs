﻿namespace FindATrade.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public Service()
        {
            this.Packages = new HashSet<Package>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [StringLength(ServiceConstants.TitleMax, MinimumLength = ServiceConstants.TitleMin, ErrorMessage = ServiceConstants.TitleMessage)]
        public string Title { get; set; }

        public bool IsPremium { get; set; }

        [Required]
        [MinLength(ServiceConstants.DescriptionMin, ErrorMessage = ServiceConstants.DescriptionMessage)]
        public string Description { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public int? VettingId { get; set; }

        public Vetting Vetting { get; set; }

        public int? PaidOrderId { get; set; }

        public PaidOrder PaidOrder { get; set; }

        public ICollection<Package> Packages { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
