namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class PackageModel : IMapFrom<Package>
    {
        [Display(Name = "Price of service e.g. 100.00 lv")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be between 0 and 10 milion")]
        public decimal? Price { get; set; }

        [Display(Name = "Description of ofered service for that price")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 300 characters")]
        public string Description { get; set; }
    }
}
