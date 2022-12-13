namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class PackageModel : IMapFrom<Package>
    {
        [Display(Name = PackageConstants.PriceName)]
        [Range(PackageConstants.PriceMin, PackageConstants.PriceMax, ErrorMessage = PackageConstants.PriceMessage)]
        public decimal? Price { get; set; }

        [Display(Name = PackageConstants.DescriptionName)]
        [StringLength(PackageConstants.DescriptionMax, MinimumLength = PackageConstants.DescriptionMin, ErrorMessage = PackageConstants.DescriptionMessage)]
        public string Description { get; set; }
    }
}
