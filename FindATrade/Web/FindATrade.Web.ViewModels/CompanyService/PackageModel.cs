namespace FindATrade.Web.ViewModels.CompanyService
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class PackageModel
    {
        [Required]
        [Display(Name = "Price of service e.g. 100.00 lv")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Description of ofered service for that price")]
        public string Description { get; set; }
    }
}
