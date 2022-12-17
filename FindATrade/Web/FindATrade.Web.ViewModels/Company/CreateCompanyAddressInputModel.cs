namespace FindATrade.Web.ViewModels.Company
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class CreateCompanyAddressInputModel : IMapFrom<Address>
    {
        [StringLength(AddressConstants.StreetMax, MinimumLength = AddressConstants.StreetMin, ErrorMessage = AddressConstants.StreetMessage)]
        [Required]
        public string Street { get; set; }

        [Display(Name = AddressConstants.HouseName)]
        [Range(AddressConstants.HouseMin, AddressConstants.HouseMax, ErrorMessage = AddressConstants.HouseMessage)]
        [Required]
        public int HouseNumber { get; set; }

        [Display(Name = AddressConstants.HouseAdditionName)]
        [StringLength(AddressConstants.HouseAdditionMax, MinimumLength = AddressConstants.HouseAdditionMin, ErrorMessage = AddressConstants.HouseAdditionMessage)]
        public string HouseNumberAddition { get; set; }

        [Required]
        [StringLength(AddressConstants.CityMax, MinimumLength = AddressConstants.CityMin, ErrorMessage = AddressConstants.CityMessage)]
        public string City { get; set; }

        [Required]
        [StringLength(AddressConstants.CountryMax, MinimumLength = AddressConstants.CountryMin, ErrorMessage = AddressConstants.CountryMessage)]
        public string Country { get; set; }

        [Display(Name = AddressConstants.PostalCodeName)]
        public string PostalCode { get; set; }
    }
}
