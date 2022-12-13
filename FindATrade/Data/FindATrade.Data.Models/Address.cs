namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        [StringLength(AddressConstants.StreetMax, MinimumLength = AddressConstants.StreetMin, ErrorMessage = AddressConstants.StreetMessage)]
        public string Street { get; set; }

        [Range(AddressConstants.HouseMin, AddressConstants.HouseMax, ErrorMessage = AddressConstants.HouseMessage)]
        public int HouseNumber { get; set; }

        [StringLength(AddressConstants.HouseAdditionMax, MinimumLength = AddressConstants.HouseAdditionMin, ErrorMessage = AddressConstants.HouseAdditionMessage)]
        public string HouseNumberAddition { get; set; }

        [StringLength(AddressConstants.CityMax, MinimumLength = AddressConstants.CityMin, ErrorMessage = AddressConstants.CityMessage)]
        public string City { get; set; }

        [StringLength(AddressConstants.CountryMax, MinimumLength = AddressConstants.CountryMin, ErrorMessage = AddressConstants.CountryMessage)]
        public string Country { get; set; }

        public string PostalCode { get; set; }

        public Company Company { get; set; }
    }
}