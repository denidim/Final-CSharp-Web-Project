namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Package : BaseDeletableModel<int>
    {
        [Range(PackageConstants.PriceMin, PackageConstants.PriceMax, ErrorMessage = PackageConstants.PriceMessage)]
        public decimal? Price { get; set; }

        [StringLength(PackageConstants.DescriptionMax, MinimumLength = PackageConstants.DescriptionMin, ErrorMessage = PackageConstants.DescriptionMessage)]
        public string Description { get; set; }

        public int? ServiceId { get; set; }

        public Service Service { get; set; }
    }
}
