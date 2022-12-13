namespace FindATrade.Web.ViewModels.Company
{
    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CreateCompanyAddressInputModel : IMapFrom<Address>
    {
        [StringLength(GlobalConstants, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Required]
        public string Street { get; set; }

        [Display(Name = "House/Flat Number")]
        [Range(0, 1000, ErrorMessage = "{0} must be netween {1} and {2}")]
        [Required]
        public int HouseNumber { get; set; }

        [Display(Name = "Addition (optional)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string HouseNumberAddition { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Country { get; set; }

        [Display(Name = "Postal Code (optional)")]
        public string PostalCode { get; set; }
    }
}
