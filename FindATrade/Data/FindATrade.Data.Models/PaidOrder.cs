namespace FindATrade.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class PaidOrder : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(PaidOrderConstants.NameMax, MinimumLength = PaidOrderConstants.NameMin, ErrorMessage = PaidOrderConstants.NameMessage)]
        public string Name { get; set; }

        [Required]
        [Range(PaidOrderConstants.PriceMin, PaidOrderConstants.PriceMax, ErrorMessage = PaidOrderConstants.PriceMessage)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(PaidOrderConstants.NameMax, MinimumLength = PaidOrderConstants.NameMin, ErrorMessage = PaidOrderConstants.NameMessage)]
        public string Terms { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public Service Service { get; set; }
    }
}
