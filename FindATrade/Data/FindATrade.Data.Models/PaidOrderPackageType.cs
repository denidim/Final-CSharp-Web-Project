namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class PaidOrderPackageType : BaseDeletableModel<int>
    {
        public int? PaidOrderId { get; set; }

        public PaidOrder PaidOrders { get; set; }

        [Required]
        [StringLength(PaidOrderPackageConstants.NameMax, MinimumLength = PaidOrderPackageConstants.NameMin, ErrorMessage = PaidOrderPackageConstants.PriceMessage)]
        public string Name { get; set; }

        [Required]
        [Range(PaidOrderPackageConstants.PriceMin, PaidOrderPackageConstants.PriceMax, ErrorMessage = PaidOrderPackageConstants.PriceMessage)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(PaidOrderPackageConstants.TermsMax, MinimumLength = PaidOrderPackageConstants.TermsMin, ErrorMessage = PaidOrderPackageConstants.TermsMesage)]
        public string Terms { get; set; }
    }
}