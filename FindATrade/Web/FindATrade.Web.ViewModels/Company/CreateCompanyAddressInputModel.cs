namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CreateCompanyAddressInputModel : IMapFrom<Address>
    {
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Street { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Number")]
        [Required]
        public int HouseNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Addition (optional)")]
        public string HouseNumberAddition { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Postal Code (optional)")]
        public string PostalCode { get; set; }
    }
}
